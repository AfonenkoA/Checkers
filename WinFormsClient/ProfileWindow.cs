using System.Drawing;
using System.Windows.Forms;
using Checkers.Transmission;
using System.Resources;

namespace WinFormsClient
{
    public partial class ProfileWindow : Form
    {
        private static readonly ResourceManager manager = Properties.Resources.ResourceManager;
        private UserInfo _info;

        private void UpdateElements()
        {
            NickLabel.Text = _info.Nick;
            RaitingLabel.Text = _info.Raiting.ToString();
            LastActivityLabel.Text = _info.LastActivity.ToString();
            ProfilePictureBox.Image = (Image)manager.GetObject($"ProfilePic{_info.PictureID}");
        }

        public ProfileWindow(UserInfo info)
        {
            _info = info;
            InitializeComponent();
            UpdateElements();
            Show();
        }

    }
}
