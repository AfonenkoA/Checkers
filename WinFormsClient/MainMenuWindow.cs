using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WinFormsClient
{
    public partial class MainMenuWindow : Form
    {
        private readonly LoginWindow _mainAppWindow;
        private readonly GameSelectionWindow _gameSelectionWindow;
        private readonly CollectionWindow _collectionWindow;
        private readonly ShopWindow _shopWindow;
        private readonly ProfileWindow _profileWindow;
        private readonly FriendsWindow _friendsWindow;
        public MainMenuWindow(LoginWindow loginWindow)
        {

            InitializeComponent();
            _mainAppWindow = loginWindow;
            _gameSelectionWindow = new GameSelectionWindow(this);
            _collectionWindow = new CollectionWindow(this);
            _shopWindow =new ShopWindow(this);
            _profileWindow = new ProfileWindow(this);
            _friendsWindow = new FriendsWindow(this);
        }

        private void MainMenuWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mainAppWindow.Close();
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            Hide();
            _mainAppWindow.Show();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Hide();
            _gameSelectionWindow.Show();
        }

        private void CollectionButton_Click(object sender, EventArgs e)
        {
            Hide();
            _collectionWindow.Show();
        }

        private void ShopButton_Click(object sender, EventArgs e)
        {
            Hide();
            _shopWindow.Show();
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            Hide();
            _profileWindow.Show();
        }

        private void FriendsButton_Click(object sender, EventArgs e)
        {
            Hide();
            _friendsWindow.Show();
        }
    }
}
