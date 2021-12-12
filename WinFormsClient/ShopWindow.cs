namespace WinFormsClient;

public partial class ShopWindow : Form
{
    private readonly MainMenuWindow _menuWindow;
    public ShopWindow(MainMenuWindow window)

    {
        _menuWindow = window;
        InitializeComponent();
    }

    private void ReturnButton_Click(object sender, EventArgs e)
    {
        Hide();
        _menuWindow.Show();
    }

    private void ShopWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        _menuWindow.Close();
    }
}