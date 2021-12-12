namespace WinFormsClient;

public partial class ProfileWindow : Form
{
    private readonly MainMenuWindow _menuWindow;
    public ProfileWindow(MainMenuWindow window)
    {
        _menuWindow = window;
        InitializeComponent();
    }

    private void ProfileWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        _menuWindow.Close();
    }

    private void ReturnButton_Click(object sender, EventArgs e)
    {
        Hide();
        _menuWindow.Show();
    }
}