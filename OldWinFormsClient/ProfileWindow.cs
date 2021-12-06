using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using Checkers.Game.Old;
using OldWinFormsClient.Properties;

namespace OldWinFormsClient;

internal sealed partial class ProfileWindow : Form
{
    private static readonly ResourceManager Manager = Resources.ResourceManager;
    private readonly UserInfo _info;

    private void UpdateElements()
    {
        NickLabel.Text = _info.Nick;
        RaitingLabel.Text = _info.Raiting.ToString();
        LastActivityLabel.Text = _info.LastActivity.ToString(CultureInfo.InvariantCulture);
        if (Manager.GetObject($"ProfilePic{_info.PictureId}") is Image image)
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