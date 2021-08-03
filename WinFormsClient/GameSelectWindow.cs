using System.Windows.Forms;
using System;

namespace WinFormsClient
{
    public partial class GameSelectWindow : Form
    {
        private readonly MainMenuWindow _menuWindow;

        public GameSelectWindow(MainMenuWindow window)
        {
            _menuWindow = window;
            InitializeComponent();
        }

        private void BackToMenuButton_Click(object sender, EventArgs e)
        {
            Hide();
            _menuWindow.Show();
        }

        private void GameSelectWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _menuWindow.Close();
        }

        private void PlayOnlineButton_Click(object sender, EventArgs e)
        {
            Hide();
            new GameWindow(this).Show();
        }
    }
}
