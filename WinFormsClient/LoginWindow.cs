using Checkers.Api.Interface;
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

            var c = new Credential
                {Login = LoginTextBox.Text, Password = PasswordTextBox.Text};
            if (await UserApi.Authenticate(c))
            {
                Hide();
                new MainMenuWindow(c,this).Show();

            }
            else
            {
                MessageBox.Show(this, "Authorization error", "Error");
            }

        }
    }
}
