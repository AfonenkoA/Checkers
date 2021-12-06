using System;
using System.Windows.Forms;
using Checkers.Game.Old;
using static OldWinFormsClient.Common;
using EventArgs = System.EventArgs;

namespace OldWinFormsClient;

public sealed partial class LoginWindow : Form
{
    internal LoginWindow()
    {
        InitializeComponent();
    }

    private async void EnterButton_Click(object sender, EventArgs e)
    {
        RegisterClient(LoginTextBox.Text, PasswordTextBox.Text);
        try
        {
            UserAuthorizationResponse response = await Client?.AuthorizeAsync()!;
            if (response.Status == ResponseStatus.Failed)
                throw new Exception("AuthorizationException, invalid login or password");
            Hide();
            new MainMenuWindow(this).Show();
        }
        catch (Exception)
        {
            MessageBox.Show(this, "Authorization error", "Error");
        }

    }

    private void LoginWindow_Load(object sender, EventArgs e)
    {

    }
}