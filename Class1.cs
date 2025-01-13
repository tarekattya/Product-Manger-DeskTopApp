using Microsoft.Data.SqlClient;
using System;
using System.Data;

public class BusinessLogicLayer
{
    private DataAccessLayer _dal;

    public BusinessLogicLayer(string connectionString)
    {
        _dal = new DataAccessLayer(connectionString);
    }

    #region User Operations

    // Register a new user
    public bool RegisterUser(string firstName, string lastName, string username, string email, string password, string role)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("All fields are required.");
        }

        using (SqlDataReader reader = _dal.GetUserByUsername(username))
        {
            if (reader.HasRows)
            {
                return false; // Username already exists
            }
        }

        string hashedPassword = HashPassword(password);
        return _dal.RegisterUser(firstName, lastName, username, email, hashedPassword, role);
    }

    // Login user
    // Login user and check role
    public (bool IsAuthenticated, string Role) LoginUserWithRole(string username, string password)
    {
        // Validate credentials
        SqlDataReader reader = _dal.GetUserByUsername(username);
        if (!reader.HasRows)
        {
            return (false, null);  // User not found
        }

        reader.Read();
        string storedPassword = reader["PasswordHashed"].ToString();  // Get the stored password from DB
        string role = reader["User_Role"].ToString();  // Get the role from DB

        // Verify the password (this assumes you are using a hashing function)
        if (VerifyPassword(password, storedPassword))
        {
            return (true, role);  // Login successful, return role
        }

        return (false, null);  // Invalid password
    }


    // Update user
    public bool UpdateUserRole(int userId, string newRole)
    {
        if (string.IsNullOrWhiteSpace(newRole))
        {
            throw new ArgumentException("Role cannot be empty.");
        }

        return _dal.UpdateUserRole(userId, newRole);
    }

    //update roles
    public List<string> GetAllRoles()
    {
        List<string> roles = new List<string>();
        SqlDataReader reader = _dal.GetAllRoles(); 

        while (reader.Read())
        {
            roles.Add(reader["User_Role"].ToString()); 
        }
        reader.Close();
        return roles;
    }


    // Delete user
    public bool DeleteUser(int userId)
    {
        return _dal.DeleteUser(userId);
    }

    public List<Dictionary<string, object>> GetAllUsers()
    {
        SqlDataReader reader = _dal.GetAllUsers();
        List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();

        while (reader.Read())
        {
            Dictionary<string, object> user = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                user.Add(reader.GetName(i), reader.GetValue(i));
            }
            users.Add(user);
        }
        reader.Close();

        return users;
    }

    #endregion

    #region Category Operations





 
    public List<Dictionary<string, object>> GetAllCategories()
    {
        
        SqlDataReader categoriesReader = _dal.GetAllCategories();

        List<Dictionary<string, object>> categories = new List<Dictionary<string, object>>();

        
        while (categoriesReader.Read())
        {
            Dictionary<string, object> category = new Dictionary<string, object>();

            
            for (int i = 0; i < categoriesReader.FieldCount; i++)
            {
                string columnName = categoriesReader.GetName(i); 
                object value = categoriesReader.GetValue(i);      
                category.Add(columnName, value);  
            }

            categories.Add(category); 
        }

        categoriesReader.Close();  

        return categories;  
    }




    // Add a new category
    public bool AddCategory(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            throw new ArgumentException("Category name cannot be empty.");
        }

        return _dal.AddCategory(categoryName);
    }

    // Update category

    public bool UpdateCategory(int categoryId, string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            throw new ArgumentException("Category name cannot be empty.");
        }

        return _dal.UpdateCategory(categoryId, categoryName);
    }

    // Delete category
    public bool DeleteCategory(int categoryId)
    {
        return _dal.DeleteCategory(categoryId);
    }

    #endregion

    #region Product Operations

    //get all product
    public DataTable GetAllProducts()
    {
        return _dal.GetAllProductsFromReader();
    }


    // Add a new product
    public bool AddProduct(string productName, decimal price, int categoryId)
    {
        if (string.IsNullOrWhiteSpace(productName) || price <= 0)
        {
            throw new ArgumentException("Product name and price must be valid.");
        }

        return _dal.AddProduct(productName, price, categoryId);
    }

    // Update product
    public bool UpdateProduct(int productId, string productName, decimal price, int categoryId)
    {
        if (string.IsNullOrWhiteSpace(productName) || price <= 0)
        {
            throw new ArgumentException("Product name and price must be valid.");
        }

        return _dal.UpdateProduct(productId, productName, price, categoryId);
    }

    // Delete product
    public bool DeleteProduct(int productId)
    {
        return _dal.DeleteProduct(productId);
    }

    #endregion

    #region Favorite Operations

    public List<Dictionary<string, object>> GetCompleteFavorites()
    {
        SqlDataReader reader = _dal.GetCompleteFavorites();
        List<Dictionary<string, object>> favorites = new List<Dictionary<string, object>>();

        while (reader.Read())
        {
            Dictionary<string, object> favorite = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                favorite.Add(reader.GetName(i), reader.GetValue(i));
            }
            favorites.Add(favorite);
        }
        reader.Close();

        return favorites;
    }





    // Add a product to the user's favorites
    public bool AddToFavorites(int userId, int productId)
    {
        return _dal.AddToFavorites(userId, productId);
    }

    // Remove a product from the user's favorites
    public bool RemoveFromFavorites(int userId, int productId)
    {
        return _dal.DeleteFromFavorites(userId, productId);
    }

    #endregion

    #region Password Hashing

    private string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    private bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        string hashedEnteredPassword = HashPassword(enteredPassword);
        return hashedEnteredPassword == storedPassword;
    }

    #endregion
}
