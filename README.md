# Product Management System

## Overview
The **Product Management System** is a Windows Forms application developed with a 3-layer architecture (Presentation, Business Logic, and Data Access). It allows administrators to manage users, categories, and products effectively. This system leverages ADO.NET for database communication and adheres to best practices for modularity and scalability.

## Features

### User Management
- **Update User Roles**: Modify the role of an existing user.
- **Authentication**: Secure login for administrators and users.

### Category Management
- **Add Categories**: Add new product categories.
- **Update Categories**: Modify existing categories.
- **Delete Categories**: Remove categories (with validation to prevent deletion if linked to products).
- **View All Categories**: Display all categories in a grid.

### Product Management
- **Add Products**: Add new products with associated categories.
- **Update Products**: Modify existing product details.
- **Delete Products**: Remove products.
- **View All Products**: Display all products, including category details.

### Favorites Management
- **Add to Favorites**: Add products to a user's favorites.
- **Remove from Favorites**: Remove products from favorites.
- **View Favorites**: Display all favorite products for a user.

## Technologies Used
- **C#**: Core programming language.
- **ADO.NET**: Data access technology for database communication.
- **SQL Server**: Backend database.
- **Windows Forms**: User interface framework.

## Architecture
The project follows a **3-layer architecture**:

1. **Presentation Layer**
   - Handles the user interface and user interactions.
   - Communicates with the Business Logic Layer (BLL).

2. **Business Logic Layer (BLL)**
   - Contains the core business rules and logic.
   - Validates user inputs and handles exceptions.
   - Interacts with the Data Access Layer (DAL).

3. **Data Access Layer (DAL)**
   - Responsible for all database operations.
   - Uses ADO.NET to execute queries and stored procedures.

## Database Design
### Tables
- **Sys_User**: Stores user information.
  - Columns: UserId (PK), UserName, Email, PasswordHashed, User_Role
- **Categories**: Stores category information.
  - Columns: CategoryId (PK), CategoryName
- **Products**: Stores product information.
  - Columns: ProductId (PK), ProductName, Price, CategoryId (FK)
- **Favorites**: Stores favorite product associations.
  - Columns: FavoriteId (PK), UserId (FK), ProductId (FK)

## How to Run the Project
1. Clone the repository:
   ```bash
   git clone https://github.com/tarekattya/product-management-system.git
   ```

2. Open the solution file (`.sln`) in Visual Studio.

3. Update the database connection string in the `DataAccessLayer` class:
   ```csharp
   private readonly string _connectionString = "YourConnectionStringHere";
   ```

4. Build and run the project.

5. Ensure the database is set up correctly with the provided schema.

## Example Usage
### Adding a Product
1. Populate the category dropdown from the `Categories` table.
2. Enter product details (e.g., name, price, category).
3. Click "Add Product" to save the product to the database.

### Updating a Category
1. Select a category from the DataGridView.
2. Modify the category name in the input field.
3. Click "Update Category" to save changes.

## Screenshots
### Login Form
![Login Form](#)

### Product Management Form
![Product Management](#)

## Contributions
Feel free to contribute to this project! Follow these steps:
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your message here"
   ```
4. Push the branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Create a pull request.

## License
This project is licensed under the [MIT License](LICENSE).

## Contact
For any inquiries or feedback, please contact:
- **Name**: Tarek Attya
- **Email**: ta422002@gmail.com
- **GitHub**: https://github.com/tarekattya

