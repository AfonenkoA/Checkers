using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;

namespace WinFormsClient
{
    public partial class MainMenuWindow : Form
    {
        private static readonly IAsyncUserApi UserApi = new UserWebApi();
        private static readonly IAsyncItemApi ItemApi = new ItemWebApi();

        private readonly LoginWindow _mainAppWindow;
        private readonly GameSelectionWindow _gameSelectionWindow;
        private readonly CollectionWindow _collectionWindow;
        private ShopWindow _shopWindow;

        private ProfileWindow? _profileWindow;

        private readonly FriendsWindow _friendsWindow;
        private readonly Credential _credential;

        private ProfileWindow Profile => _profileWindow ?? (_profileWindow = new(_credential, this));


        public MainMenuWindow(Credential c,LoginWindow loginWindow)
        {

            InitializeComponent();
            _credential = c;
            _mainAppWindow = loginWindow;
            _gameSelectionWindow = new GameSelectionWindow(this);
            _collectionWindow = new CollectionWindow(this);
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

        private async void ShopButton_Click(object sender, EventArgs e)
        {

            for(int i =0; i < 5; i++)
                await ItemApi.TryGetCheckersSkin(1).ConfigureAwait(false);
            Hide();
            return;
            
            var (_, user) = await UserApi.TryGetSelf(_credential).ConfigureAwait(false);
            var (_, checkers) = await UserApi.TryGetAvailableCheckers(_credential).ConfigureAwait(false);
            var (_, animations) = await UserApi.TryGetAvailableAnimations(_credential).ConfigureAwait(false);
            var (_, lootbox) = await UserApi.TryGetAvailableLootBoxes(_credential).ConfigureAwait(false);
            //checkers
            var l1 = new List<Control>();
            foreach (var i in checkers)
            {
                var (_, skin) = await ItemApi.TryGetCheckersSkin(i).ConfigureAwait(false);
                l1.Add(new ItemShowPanel(skin));
            }
            var l2 = new List<Control>();
            //lootbox
            foreach (var i in lootbox)
            {
                var (_, box) = await ItemApi.TryGetLootBox(i).ConfigureAwait(false);
                l2.Add(new ItemShowPanel(box));
            }
            var l3 = new List<Control>();
            //animation
            foreach (var i in animations)
            {
                var (_, animation) = await ItemApi.TryGetAnimation(i).ConfigureAwait(false);
                l3.Add(new ItemShowPanel(animation));
            }
            _shopWindow = new ShopWindow(l1,l2,l3);
            Hide();
            _shopWindow.Show();
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            Hide();
            Profile.Show();
        }

        private void FriendsButton_Click(object sender, EventArgs e)
        {
            Hide();
            _friendsWindow.Show();
        }
    }
}
