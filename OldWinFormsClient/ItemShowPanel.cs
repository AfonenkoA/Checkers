using System.Drawing;
using System.Windows.Forms;

namespace OldWinFormsClient;

internal sealed partial class ItemShowPanel : UserControl
{
    private static readonly System.Resources.ResourceManager Manager = Properties.Resources.ResourceManager;
    public ItemShowPanel(string img, string title, string desc)
    {
        InitializeComponent();
        if (Manager.GetObject(img) is Image image)
            PictureBox.Image = image;
        TitleLabel.Text = Manager.GetString(title);
        DescriptionLabel.Text = Manager.GetString(desc);
    }
}