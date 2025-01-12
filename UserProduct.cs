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
    public partial class UserProduct : Form
    {

        private const string connectionString = "Server=.;Database=CommerceDT;Integrated Security=True;TrustServerCertificate=True;";
        BusinessLogicLayer businessLogicLayer = new BusinessLogicLayer(connectionString);
        public UserProduct()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0)
                {

                    DataGridViewRow selectedRow = UProductDGV.Rows[e.RowIndex];




                    NameProT.Text = selectedRow.Cells["ProductName"].Value.ToString();
                    PriceProBoxT.Text = selectedRow.Cells["Price"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void UserProduct_Load(object sender, EventArgs e)
        {
            PopulateProductGrid();

        }
        private void PopulateProductGrid()
        {
            try
            {
                DataTable products = businessLogicLayer.GetAllProducts();
                UProductDGV.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            HomeUser homeUser = new HomeUser();
            homeUser.Show();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            try
            {

                if (UProductDGV.SelectedRows.Count > 0)
                {

                    int productId = Convert.ToInt32(UProductDGV.SelectedRows[0].Cells["ProductId"].Value);
                    int userId = 1;


                    bool isAdded = businessLogicLayer.AddToFavorites(userId, productId);


                    if (isAdded)
                    { 
                        MessageBox.Show("Product added to favorites!");
                        PopulateProductGrid();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product to favorites.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a product to add to your favorites.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void FavBTN_Click(object sender, EventArgs e)
        {
            FavProducts favProducts = new FavProducts();
            favProducts.Show();
            this.Close();
        }
    }
}

