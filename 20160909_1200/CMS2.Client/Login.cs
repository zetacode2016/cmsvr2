using System;
using System.Windows.Forms;

namespace CMS2.Client
{
    public partial class Login : Form
    {
        public string username="";
        public string password = "";
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            CaptureCredentials();
        }

        private void bntCancel_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void txtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
            }
        }

        private void btnLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CaptureCredentials();
            }
        }

        private void CaptureCredentials()
        {
            username = txtUsername.Text.Trim();
            password = txtPassword.Text.Trim();
            
        }
    }
}
