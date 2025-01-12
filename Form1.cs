namespace Tatuuu_s_Store
{
    public partial class Login : Form
    {
        public string NameUser;
        private const string connectionString = "Server=.;Database=CommerceDT;Integrated Security=True;TrustServerCertificate=True;";
        BusinessLogicLayer businessLogicLayer = new BusinessLogicLayer(connectionString);

        public Login()
        {
            InitializeComponent();
        }

        private void LINKREG_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Create an instance of the Registration Form
            Registeration registrationForm = new Registeration();

            // Show the Registration Form
            registrationForm.Show();

            // Optionally, hide the Login Form
            this.Hide();
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            var loginResult = businessLogicLayer.LoginUserWithRole(UName.Text, Password.Text);


            if (loginResult.IsAuthenticated)
            {
                if (loginResult.Role == "Admin")
                {
                    DashBoard dashBoard = new DashBoard(UName.Text); // Pass the username to the dashboard
                    dashBoard.Show();
                    this.Hide();

                }
                else
                {
                    HomeUser homeUser = new HomeUser();
                    homeUser.Show();
                    this.Hide();

                }
                 
            }
            else
            {
                MessageBox.Show("Login failed. Please check your username and password.");
            }


        
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
