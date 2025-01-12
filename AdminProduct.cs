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

    public partial class AdminProduct : Form
    {

        private const string connectionString = "Server=.;Database=CommerceDT;Integrated Security=True;TrustServerCertificate=True;";
        BusinessLogicLayer businessLogicLayer = new BusinessLogicLayer(connectionString);
        public AdminProduct()
        {
            InitializeComponent();
        }

        private void ProductPage_Click(object sender, EventArgs e)
        {

        }

        private void UserSpage_Click(object sender, EventArgs e)
        {
            AdminUser user = new AdminUser();
            user.Show();
            this.Close();
        }



        private void CategoriesPage_Click(object sender, EventArgs e)
        {
            AdminCategories adminCategories = new AdminCategories();
            adminCategories.Show();
            this.Close();
        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.Show();
            this.Close();
        }


        private void PopulateCategoryComboBox()
        {
            try
            {
                // Fetch categories as a List<Dictionary<string, object>>
                var categoryDicts = businessLogicLayer.GetAllCategories();

                // Transform to a list of objects with CategoryId and CategoryName properties
                var categories = categoryDicts.Select(cat => new
                {
                    CategoryId = Convert.ToInt32(cat["CategoryId"]),
                    CategoryName = cat["CategoryName"].ToString()
                }).ToList();

                // Bind the transformed data
                CateComBox.DataSource = categories;
                CateComBox.DisplayMember = "CategoryName";
                CateComBox.ValueMember = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
            }
        }


        private void AdminProduct_Load(object sender, EventArgs e)
        {
            PopulateCategoryComboBox();
            PopulateProductGrid();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            try
            {
                string productName = NameProT.Text;
                decimal price = decimal.Parse(PriceProBoxT.Text);
                int categoryId = Convert.ToInt32(CateComBox.SelectedValue);

                bool isAdded = businessLogicLayer.AddProduct(productName, price, categoryId);

                if (isAdded)
                {
                    MessageBox.Show("Product added successfully.");
                    PopulateProductGrid();
                }
                else
                    MessageBox.Show("Failed to add product.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void PopulateProductGrid()
        {
            try
            {
                DataTable products = businessLogicLayer.GetAllProducts();
                ProDuctDGV.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected
                if (ProDuctDGV.SelectedRows.Count > 0)
                {
                    // Get the Product ID from the selected row
                    int productId = Convert.ToInt32(ProDuctDGV.SelectedRows[0].Cells["ProductId"].Value);

                    // Ask for confirmation before deleting
                    var confirmResult = MessageBox.Show("Are you sure you want to delete this product?",
                                                        "Confirm Delete",
                                                        MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        // Call the BLL method to delete the product
                        bool isDeleted = businessLogicLayer.DeleteProduct(productId);

                        if (isDeleted)
                        {

                            MessageBox.Show("Product deleted successfully.");
                            PopulateProductGrid();
                        }
                        else
                            MessageBox.Show("Failed to delete product. It may not exist or has related data.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a product to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ProDuctDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0)
                {

                    DataGridViewRow selectedRow = ProDuctDGV.Rows[e.RowIndex];




                    NameProT.Text = selectedRow.Cells["ProductName"].Value.ToString();
                    PriceProBoxT.Text = selectedRow.Cells["Price"].Value.ToString();

                    // Populate Category ComboBox with selected category
                    // Assuming the ComboBox contains categories with ValueMember as "CategoryId" and DisplayMember as "CategoryName"
                    CateComBox.SelectedValue = selectedRow.Cells["CategoryId"].Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {



            {
                try
                {
                    // Ensure a row is selected in the DataGridView
                    if (ProDuctDGV.SelectedRows.Count > 0)
                    {
                        // Get the selected row's data
                        DataGridViewRow selectedRow = ProDuctDGV.SelectedRows[0];

                        // Fetch ProductId from the selected row
                        int productId = Convert.ToInt32(selectedRow.Cells["ProductId"].Value);

                        // Fetch other product details from the textboxes and combobox
                        string productName = NameProT.Text;
                        decimal price = decimal.Parse(PriceProBoxT.Text);
                        int categoryId = (int)CateComBox.SelectedValue;

                        // Call the business logic layer to update the product
                        bool isUpdated = businessLogicLayer.UpdateProduct(productId, productName, price, categoryId);

                        // Show the result of the update
                        if (isUpdated)
                        {

                            MessageBox.Show("Product updated successfully.");
                            PopulateProductGrid();
                        }
                        else
                            MessageBox.Show("Failed to update the product.");
                    }
                    else
                    {
                        MessageBox.Show("Please select a product from the grid.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            NameProT.Text = string.Empty;
            PriceProBoxT.Text= string.Empty;


        }
    }
}



 


