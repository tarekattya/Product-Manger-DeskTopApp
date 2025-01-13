using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.Common;


public class DataAccessLayer
{
    private readonly string _connectionString;

    public DataAccessLayer(string connectionString)
    {
        _connectionString = connectionString;
    }

    #region User Operations

    // Get user by username for login
    public SqlDataReader GetUserByUsername(string username)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM Sys_User WHERE UserName = @UserName";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserName", username);
        connection.Open();
        return command.ExecuteReader();
    }

    // Register new user
    public bool RegisterUser(string firstName, string lastName, string username, string email, string password, string role)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "INSERT INTO Sys_User (FirstName, LastName, UserName, Email, PasswordHashed, User_Role) " +
                       "VALUES (@FirstName, @LastName, @UserName, @Email, @Password, @Role)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@FirstName", firstName);
        command.Parameters.AddWithValue("@LastName", lastName);
        command.Parameters.AddWithValue("@UserName", username);
        command.Parameters.AddWithValue("@Email", email);
        command.Parameters.AddWithValue("@Password", password);
        command.Parameters.AddWithValue("@Role", role);

        connection.Open();
        int result = command.ExecuteNonQuery();
        return result > 0;
    }

    //update roles
    public bool UpdateUserRole(int userId, string newRole)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "UPDATE Sys_User SET User_Role = @Role WHERE Id = @UserId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Role", newRole);
        cmd.Parameters.AddWithValue("@UserId", userId);

        try
        {
            connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            // Handle exception (could log or rethrow if necessary)
            throw new Exception("Error updating user role", ex);
        }
        finally
        {
            connection.Close(); // Ensure connection is closed in case of exceptions
        }
    }


    // Delete user
    public bool DeleteUser(int userId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "DELETE FROM Sys_User WHERE UserId = @UserId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", userId);

        connection.Open();
        int result = command.ExecuteNonQuery();
        connection.Close();
        return result > 0;
    }

    public SqlDataReader GetAllUsers()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM Sys_User";
        SqlCommand command = new SqlCommand(query, connection);
        connection.Open();
        return command.ExecuteReader();
    }
    #endregion

    #region Category Operations

    // Get all categories
    public SqlDataReader GetAllCategories()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM Categories";
        SqlCommand command = new SqlCommand(query, connection);
        connection.Open();
        return command.ExecuteReader();
    }

    // Add a new category
    public bool AddCategory(string categoryName)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@CategoryName", categoryName);

        connection.Open();
        int result = command.ExecuteNonQuery();
        return result > 0;
    }

    // Update category
    public bool UpdateCategory(int categoryId, string categoryName)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@CategoryId", categoryId);
        command.Parameters.AddWithValue("@CategoryName", categoryName);

        connection.Open();
        int result = command.ExecuteNonQuery();
        connection.Close();
        return result > 0;
    }

    // Delete category
    public bool DeleteCategory(int categoryId)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
               
                string deleteProductsQuery = "DELETE FROM Products WHERE CategoryId = @CategoryId";
                using (SqlCommand deleteProductsCommand = new SqlCommand(deleteProductsQuery, connection, transaction))
                {
                    deleteProductsCommand.Parameters.AddWithValue("@CategoryId", categoryId);
                    deleteProductsCommand.ExecuteNonQuery();
                }

                
                string deleteCategoryQuery = "DELETE FROM Categories WHERE CategoryId = @CategoryId";
                using (SqlCommand deleteCategoryCommand = new SqlCommand(deleteCategoryQuery, connection, transaction))
                {
                    deleteCategoryCommand.Parameters.AddWithValue("@CategoryId", categoryId);
                    deleteCategoryCommand.ExecuteNonQuery();
                }

                // Commit transaction
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                // Rollback transaction in case of error
                transaction.Rollback();
                return false;
            }
        }
    }

    public SqlDataReader GetAllRoles()
    {
        SqlConnection connection = new SqlConnection(_connectionString);

        try
        {
            connection.Open(); // Open the connection
            string query = "SELECT DISTINCT User_Role FROM Sys_User";
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader(CommandBehavior.CloseConnection); // Ensures the connection closes when the reader is closed
        }
        catch (Exception ex)
        {
            connection.Dispose(); // Dispose of the connection in case of an error
            throw new Exception($"Error retrieving roles: {ex.Message}");
        }
    }



    #endregion

    #region Product Operations

    // Get all products
    public DataTable GetAllProductsFromReader()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM Products";
        SqlCommand command = new SqlCommand(query, connection);

        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        DataTable productTable = new DataTable();

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Load data from SqlDataReader into DataTable
            productTable.Load(reader);
        }
        
        finally
        {
            connection.Close();
        }

        return productTable;
    }


    // Get products by category
    public SqlDataReader GetProductsByCategory(int categoryId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM Products WHERE CategoryId = @CategoryId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@CategoryId", categoryId);
        connection.Open();
        return command.ExecuteReader();
    }

    //Get Product By Id
    public SqlDataReader GetProductById(int productId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM Products WHERE ProductId = @ProductId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ProductId", productId);
        connection.Open();
        return command.ExecuteReader();
    }

    // Add a new product
    public bool AddProduct(string productName, decimal price, int categoryId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "INSERT INTO Products (ProductName, Price, CategoryId) VALUES (@ProductName, @Price, @CategoryId)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ProductName", productName);
        command.Parameters.AddWithValue("@Price", price);
        command.Parameters.AddWithValue("@CategoryId", categoryId);

        connection.Open();
        int result = command.ExecuteNonQuery();
        return result > 0;
    }
    //update Product

    public bool UpdateProduct(int productId, string productName, decimal price, int categoryId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "UPDATE Products SET ProductName = @ProductName, Price = @Price, CategoryId = @CategoryId " +
                       "WHERE ProductId = @ProductId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ProductId", productId);
        command.Parameters.AddWithValue("@ProductName", productName);
        command.Parameters.AddWithValue("@Price", price);
        command.Parameters.AddWithValue("@CategoryId", categoryId);

        connection.Open();
        int result = command.ExecuteNonQuery();
        connection.Close();
        return result > 0;
    }
    // Delete product
    public bool DeleteProduct(int productId)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
              
                string deleteFromFavoritesQuery = "DELETE FROM Favorites WHERE ProductId = @ProductId";
                using (SqlCommand deleteFavoritesCommand = new SqlCommand(deleteFromFavoritesQuery, connection, transaction))
                {
                    deleteFavoritesCommand.Parameters.AddWithValue("@ProductId", productId);
                    deleteFavoritesCommand.ExecuteNonQuery();
                }

               
                string deleteFromProductsQuery = "DELETE FROM Products WHERE ProductId = @ProductId";
                using (SqlCommand deleteProductsCommand = new SqlCommand(deleteFromProductsQuery, connection, transaction))
                {
                    deleteProductsCommand.Parameters.AddWithValue("@ProductId", productId);
                    deleteProductsCommand.ExecuteNonQuery();
                }

               
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                
                transaction.Rollback();
                return false;
            }
        }
    }

    #endregion

    #region Favorite Operations

    // Get all favorites for a user
    public SqlDataReader GetCompleteFavorites()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = @"
        SELECT 
            f.UserId,
            f.ProductId,
            p.ProductName,
            p.Price
        FROM 
            Favorites f
        INNER JOIN 
            Products p ON f.ProductId = p.ProductId";

        SqlCommand command = new SqlCommand(query, connection);
        connection.Open();
        return command.ExecuteReader();
    }



    // Add a product to favorites for a user
    public bool AddToFavorites(int userId, int productId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "INSERT INTO Favorites (UserId, ProductId) VALUES (@UserId, @ProductId)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", userId);
        command.Parameters.AddWithValue("@ProductId", productId);

        connection.Open();
        int result = command.ExecuteNonQuery();
        return result > 0;
    }

    // Remove a product from favorites for a user
    public bool DeleteFromFavorites(int userId, int productId)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string query = "DELETE FROM Favorites WHERE UserId = @UserId AND ProductId = @ProductId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", userId);
        command.Parameters.AddWithValue("@ProductId", productId);

        connection.Open();
        int result = command.ExecuteNonQuery();
        return result > 0;
    }

    #endregion
}
