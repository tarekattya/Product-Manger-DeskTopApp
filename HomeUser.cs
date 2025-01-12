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
    public partial class HomeUser : Form
    {
        public HomeUser()
        {
            InitializeComponent();
        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void ProductPage_Click(object sender, EventArgs e)
        {
            UserProduct product = new UserProduct();
            product.Show();
            this.Close();
        }

        private void FavBTN_Click_1(object sender, EventArgs e)
        {
            FavProducts favProducts = new FavProducts();
            favProducts.Show();
            this.Close();
        }
    }
}
