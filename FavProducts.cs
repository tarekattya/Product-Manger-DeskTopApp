using Microsoft.Data.SqlClient;
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
    public partial class FavProducts : Form
    {
        private const string connectionString = "Server=.;Database=CommerceDT;Integrated Security=True;TrustServerCertificate=True;";
        BusinessLogicLayer businessLogicLayer = new BusinessLogicLayer(connectionString);
        public FavProducts()
        {
            InitializeComponent();
        }

        private void FavProducts_Load(object sender, EventArgs e)
        {
            LoadCompleteFavorites();

        }

        private void LoadCompleteFavorites()
        {
            try
            {
                var completeFavorites = businessLogicLayer.GetCompleteFavorites();

                DataTable favoritesTable = new DataTable();

                if (completeFavorites.Count > 0)
                {
                    foreach (var key in completeFavorites[0].Keys)
                    {
                        favoritesTable.Columns.Add(key);
                    }

                    foreach (var favorite in completeFavorites)
                    {
                        DataRow row = favoritesTable.NewRow();
                        foreach (var key in favorite.Keys)
                        {
                            row[key] = favorite[key] ?? DBNull.Value;
                        }
                        favoritesTable.Rows.Add(row);
                    }

                    FavProDGV.DataSource = favoritesTable;
                }
                else
                {
                    MessageBox.Show("No favorites found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading favorites: {ex.Message}");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void ProductPage_Click(object sender, EventArgs e)
        {
            UserProduct userProduct = new UserProduct();
            userProduct.Show();
            this.Close();
        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            HomeUser user = new HomeUser();
            user.Show();
            this.Close();
        }

        private void FavProDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                // Ensure a valid cell is clicked
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = FavProDGV.Rows[e.RowIndex];

                    // Retrieve the ID from the desired column (e.g., "FavoriteId")
                    string favoriteId = selectedRow.Cells["ProductId"].Value?.ToString();

                    // Display the ID in the TextBox
                    IDProFav.Text = favoriteId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void NameProT_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
           
            try
            {
               
                if (FavProDGV.SelectedRows.Count > 0)
                {
                   
                    int userId = Convert.ToInt32(FavProDGV.SelectedRows[0].Cells["UserId"].Value);
                    int productId = Convert.ToInt32(FavProDGV.SelectedRows[0].Cells["ProductId"].Value);

                    
                    var confirmResult = MessageBox.Show("Are you sure you want to remove this product from favorites?",
                                                        "Confirm Delete",
                                                        MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                       
                        bool isDeleted = businessLogicLayer.RemoveFromFavorites(userId, productId);

                        if (isDeleted)
                        {
                            MessageBox.Show("Product removed from favorites successfully.");
                            LoadCompleteFavorites(); 
                        }
                        else
                        {
                            MessageBox.Show("Failed to remove the product from favorites. Please try again.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a favorite product to remove.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
}
}
