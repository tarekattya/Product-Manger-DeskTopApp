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
    public partial class DashBoard : Form
    {
        private string username;
        public DashBoard(string NameUseer)
        {
            InitializeComponent();
            username = NameUseer;
        }
        public DashBoard()
        {
            InitializeComponent();

        }

        private void UserSpage_Click(object sender, EventArgs e)
        {
            AdminUser user = new AdminUser();
            user.Show();
            this.Close();

        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();

        }

        private void DashBoard_Load(object sender, EventArgs e)
        {



        }

        private void CategoriesPage_Click(object sender, EventArgs e)
        {
            AdminCategories adminCategories = new AdminCategories();
            adminCategories.Show();
            this.Close();
        }


        private void ProductPage_Click(object sender, EventArgs e)
        {
            AdminProduct adminProduct = new AdminProduct();
            adminProduct.Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
