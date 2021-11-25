using System.Windows.Forms;

namespace OldWinFormsClient;

public partial class GameWindow : Form
{
    private readonly GameSelectWindow _gameSelectWindow;
    public GameWindow(GameSelectWindow window)
    {
        _gameSelectWindow = window;
        InitializeComponent();
    }

    private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        Hide();
        _gameSelectWindow.Show();
    }
}