using System.Drawing;
using System.Windows.Forms;

namespace WinFormsClient
{
    public partial class ItemShowPanel : UserControl
    {
        private static readonly System.Resources.ResourceManager manager = Properties.Resources.ResourceManager;
        public ItemShowPanel(string img,string title,string desc)
        {
            InitializeComponent();
            PictureBox.Image = (Image)manager.GetObject(img);
            TitleLabel.Text = manager.GetString(title);
            DescriptionLabel.Text = manager.GetString(desc);
        }
    }
}
