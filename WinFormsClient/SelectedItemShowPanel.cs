using WinFormsClient.Model;

namespace WinFormsClient;

public partial class SelectedItemShowPanel : UserControl
{
    public SelectedItemShowPanel(VisualDetailedItem item)
    {
        InitializeComponent();
        TitleLabel.Text = item.Name;
        DescriptionLabel.Text = item.Detail;
        pictureBox1.Image = item.Image;
    }
}