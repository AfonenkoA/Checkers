using Checkers.Api.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;

namespace WinFormsClient
{
    
    public partial class LoginWindow : Form
    {
        private static readonly IAsyncUserApi UserApi = new UserWebApi();
       
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void EnterButton_Click(object sender, EventArgs e)
        {
              
            if (await UserApi.Authenticate(new Credential
               {Login = LoginTextBox.Text, Password = PasswordTextBox.Text}))
            {
                Hide();
                new MainMenuWindow(this).Show();

            }
            else
            {
                MessageBox.Show(this, "Authorization error", "Error");
            }

        }
    }
}
