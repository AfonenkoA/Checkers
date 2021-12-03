using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using static System.Globalization.CultureInfo;

namespace WinFormsClient
{
    public partial class ProfileWindow : Form
    {
        private static readonly IAsyncUserApi UserApi = new UserWebApi();
        private static readonly IAsyncResourceService ResourceService = new AsyncResourceWebApi();
        private static readonly IAsyncItemApi ItemApi = new ItemWebApi();
        private readonly MainMenuWindow _menuWindow;
        private readonly Credential _credential;

        public ProfileWindow(Credential c,MainMenuWindow window)
        {
            _credential = c;
            _menuWindow = window;
            InitializeComponent();
            Task.Run(UpdateElements).Wait();
        }

        private async Task UpdateElements()
        {
            var (_, user) = await UserApi.TryGetSelf(_credential);
            NickLabel.Text = user.Nick;
            RatingLabel.Text = user.SocialCredit.ToString();
            MatchesLabel.Text = user.LastActivity.ToString(CurrentCulture);
            var (_, pic) = await ItemApi.TryGetPicture(user.PictureId);
            var (_, bytes) = await ResourceService.TryGetFile(pic.Resource.Id);
            var file = "1." + pic.Resource.Extension;
            await File.WriteAllBytesAsync(file,bytes);
            pictureBox1.Image = Image.FromFile(file);
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
}
