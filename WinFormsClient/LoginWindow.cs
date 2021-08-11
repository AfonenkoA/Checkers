using Checkers.Transmission;
using System;
using System.Windows.Forms;
using static WinFormsClient.Common;

namespace WinFormsClient
{
    public partial class LoginWindow : Form
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void EnterButton_Click(object sender, EventArgs e)
        {
            RegisterClient(LoginTextBox.Text, PasswordTextBox.Text);
            try
            {
                UserAuthorizationResponse response = await Client.AuthorizeAsync();
                if (response.Status == ResponseStatus.FAILED)
                    throw new Exception("AuthorizationException, invalid login or password");
                Hide();
                new MainMenuWindow(this).Show();
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Authorization error", "Error");
            }

        }

    }
}
