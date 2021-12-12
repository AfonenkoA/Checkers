namespace WinFormsClient;

public partial class CollectionWindow : Form
{
    private readonly MainMenuWindow _menuWindow;
    public CollectionWindow(MainMenuWindow window)
    {
        _menuWindow = window;
        InitializeComponent();
    }

    private void ReturnButton_Click(object sender, EventArgs e)
    {
        Hide();
        _menuWindow.Show();
    }

    private void CollectionWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        _menuWindow.Close();
    }
}