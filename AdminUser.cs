using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tatuuu_s_Store
{


    public partial class AdminUser : Form
    {

        private void LoadAllUsers()
        {
            try
            {
                List<Dictionary<string, object>> users = businessLogicLayer.GetAllUsers();
                DataTable userTable = new DataTable();

                if (users.Count > 0)
                {

                    foreach (var key in users[0].Keys)
                    {
                        userTable.Columns.Add(key);
                    }


                    foreach (var user in users)
                    {
                        DataRow row = userTable.NewRow();
                        foreach (var key in user.Keys)
                        {
                            row[key] = user[key] ?? DBNull.Value;
                        }
                        userTable.Rows.Add(row);
                    }
                    DGVAdminUser.DataSource = userTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}");
            }
        }

        private const string connectionString0 = "Server=.;Database=CommerceDT;Integrated Security=True;TrustServerCertificate=True;";
        BusinessLogicLayer businessLogicLayer = new BusinessLogicLayer(connectionString0);
        public AdminUser()
        {
            InitializeComponent();
        }

        private void AdminUser_Load(object sender, EventArgs e)
        {
            LoadAllUsers();
            PopulateRoleComboBox();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            AdminUser user = new AdminUser();
            user.Show();
            this.Close();

        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.Show();
            this.Close();
        }

        private void CatePage_Click(object sender, EventArgs e)
        {
            AdminCategories adminCategories = new AdminCategories();
            adminCategories.Show();
            this.Close();
        }

        private void ProPage_Click(object sender, EventArgs e)
        {
            AdminProduct adminProduct = new AdminProduct();
            adminProduct.Show();
            this.Close();
        }

       

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void DGVAdminUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {

                    DataGridViewRow row = DGVAdminUser.Rows[e.RowIndex];


                    string userId = row.Cells["id"].Value.ToString();


                    IdBOX.Text = userId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void PopulateRoleComboBox()
        {
            try
            {
                List<string> roles = businessLogicLayer.GetAllRoles();
                RoleComboBox.DataSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching roles: {ex.Message}");
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(IdBOX.Text);
            string newRole = RoleComboBox.Text;

            bool success = businessLogicLayer.UpdateUserRole(userId, newRole);

            if (success)
            {
                // Refresh the user role textbox directly
                RoleComboBox.Text = newRole;  // Update the TextBox to reflect the change
                MessageBox.Show("User role updated successfully.");
                LoadAllUsers();

            }
            else
            {
                MessageBox.Show("Error updating user role.");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}


