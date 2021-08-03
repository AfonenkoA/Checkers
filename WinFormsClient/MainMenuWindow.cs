using Checkers.Transmission;
using System.Windows.Forms;
using WinFormsClient.Windows;
using static System.Array;

namespace WinFormsClient
{
    public partial class MainMenuWindow : Form
    {
        private readonly LoginWindow _appMainWindow;
        private readonly GameSelectWindow _gameSelectWindow;
        private readonly AchievementsWindow _achievementsWindow;
        private readonly FriendsWindow _friendsWindow;
        private readonly CollectionWindow _collectionWindow;

        public MainMenuWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            _appMainWindow = loginWindow;
            _gameSelectWindow = new GameSelectWindow(this);
            _achievementsWindow = new AchievementsWindow(this);
            _friendsWindow = new FriendsWindow(this);
            _collectionWindow = new CollectionWindow(this);
        }

        private void MainMenuWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _appMainWindow.Close();
        }

        private void ShopButton_Click(object sender, System.EventArgs e)
        {

        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            Hide();
            _gameSelectWindow.Show();
        }

        private async void ProfileButton_Click(object sender, System.EventArgs e)
        {

            new ProfileWindow((await Common.Client.GetUserInfoAsync()).Info);
        }

        private async void AchievementsButton_Click(object sender, System.EventArgs e)
        {
            Hide();
            _achievementsWindow.Achievements = (await Common.Client.GetAchievementsAsync()).Achievements?? Empty<int>();
        }

        private async void FriendsButton_Click(object sender, System.EventArgs e)
        {
            _friendsWindow.Friends = (await Common.Client.GetFriendsAsync()).Friends;
            Hide();
        }

        private async void CollectionButton_Click(object sender, System.EventArgs e)
        {
            _collectionWindow.Items = (await Common.Client.GetItemsAsync()).Items;
            Hide();
        }
    }
}
