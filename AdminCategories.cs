using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tatuuu_s_Store
{
    public partial class AdminCategories : Form
    {

        private const string connectionString = "Server=.;Database=CommerceDT;Integrated Security=True;TrustServerCertificate=True;";
        BusinessLogicLayer businessLogicLayer = new BusinessLogicLayer(connectionString);

        public AdminCategories()
        {
            InitializeComponent();
            LoadCategories();


        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.Show();
            this.Hide();
            
        }

        private void UserSpage_Click(object sender, EventArgs e)
        {
            AdminUser user = new AdminUser();
            user.Show();
            this.Close();

        }

        private void CategoriesPage_Click(object sender, EventArgs e)
        {
            AdminCategories categories = new AdminCategories();
            categories.Show();
            this.Close();
        }

        private void ProductPage_Click(object sender, EventArgs e)
        {
            AdminProduct product = new AdminProduct();
            product.Show();
            this.Close();
        }

       
        private void GetAllCategoriesButton_Click(object sender, EventArgs e)
        {

        }




        private void LoadCategories()
        {
            try
            {
                List<Dictionary<string, object>> categories = businessLogicLayer.GetAllCategories();
                DataTable categoryTable = new DataTable();

                if (categories.Count > 0)
                {
                    foreach (var key in categories[0].Keys)
                    {
                        categoryTable.Columns.Add(key);
                    }

                    foreach (var category in categories)
                    {
                        DataRow row = categoryTable.NewRow();

                        foreach (var key in category.Keys)
                        {
                            row[key] = category[key] ?? DBNull.Value;
                        }

                        categoryTable.Rows.Add(row);
                    }

                    CateDGV.DataSource = categoryTable;
                }
                else
                {
                    MessageBox.Show("No categories found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            try
            {
                string categoryName = CateBOXName.Text; // Assuming CategoryNameTextBox exists
                bool isAdded = businessLogicLayer.AddCategory(categoryName);

                if (isAdded)
                {
                    MessageBox.Show("Category added successfully.");
                    LoadCategories();
                }
                else
                    MessageBox.Show("Failed to add category. It might already exist.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            CateBOXName.Text = "";
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (CateDGV.SelectedRows.Count > 0) // Ensure a row is selected
                {
                    // Fetch category ID from the selected row
                    int categoryId = Convert.ToInt32(CateDGV.SelectedRows[0].Cells["CategoryId"].Value);

                    // Get the updated category name from the TextBox
                    string categoryName = CateBOXName.Text;

                    if (string.IsNullOrWhiteSpace(categoryName))
                    {
                        MessageBox.Show("Category name cannot be empty.");
                        return;
                    }

                    // Call the BLL to update the category
                    bool isUpdated = businessLogicLayer.UpdateCategory(categoryId, categoryName);

                    if (isUpdated)
                    {
                        MessageBox.Show("Category updated successfully.");
                        LoadCategories();
                    }
                    else
                        MessageBox.Show("Failed to update category. Category may not exist.");
                }
                else
                {
                    MessageBox.Show("Please select a category to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void CateDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0) // Ensure it's not a header row
                {
                    // Access the selected row
                    DataGridViewRow row = CateDGV.Rows[e.RowIndex];

                    // Fetch the category name from the selected row and display it in the TextBox
                    CateBOXName.Text = row.Cells["CategoryName"].Value.ToString(); // Replace "CategoryName" with your actual column name
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {


          try
            {
                if (CateDGV.SelectedRows.Count > 0) 
                {
                    
                    int categoryId = Convert.ToInt32(CateDGV.SelectedRows[0].Cells["CategoryId"].Value); 

                  
                    var confirmResult = MessageBox.Show("Are you sure you want to delete this category?",
                                                        "Confirm Delete",
                                                        MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        
                        bool isDeleted = businessLogicLayer.DeleteCategory(categoryId);

                        if (isDeleted)
                        {
                            MessageBox.Show("Category deleted successfully.");
                            LoadCategories();
                        }
                        else
                            MessageBox.Show("Failed to delete category. It may not exist or has related products.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a category to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    

}
}






