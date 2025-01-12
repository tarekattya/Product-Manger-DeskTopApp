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
    public partial class Registeration : Form
    {


        private const string connectionString = "Server=.;Database=CommerceDT;Integrated Security=True;TrustServerCertificate=True;";
        BusinessLogicLayer businessLogicLayer = new BusinessLogicLayer(connectionString);
        public Registeration()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {


            if (businessLogicLayer.RegisterUser(FName.Text, LName.Text, UName.Text, Email.Text, Password.Text, "User"))

            {
                FName.Text = "";
                LName.Text = "";
                UName.Text = "";
                Email.Text = "";
                Password.Text = "";



                MessageBox.Show("Thank you for registering with Tatuuu's Store We're excited to have you on board.");
                BTlogin.Show();
            }

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Registeration_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BTlogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
