using Checkers.Transmission;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WinFormsClient.Properties;

namespace WinFormsClient
{
    public partial class ProfileWindow : Form
    {
        private static readonly ResourceManager manager = Resources.ResourceManager;
        private UserInfo _info;

        private void UpdateElements()
        {
            NickLabel.Text = _info.Nick;
            RaitingLabel.Text = _info.Raiting.ToString();
            LastActivityLabel.Text = _info.LastActivity.ToString();
            if (manager.GetObject($"ProfilePic{_info.PictureID}") is Image image)
                ProfilePictureBox.Image = image;
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
