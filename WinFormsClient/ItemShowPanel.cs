using WinFormsClient.Model;

namespace WinFormsClient;

internal sealed partial class ItemShowPanel : UserControl
{
    private readonly int _id;

    public ItemShowPanel(VisualDetailedItem item)
    {
        _id = item.Id;
        InitializeComponent();
        TitleLabel.Text = item.Name;
        DescriptionLabel.Text = item.Detail;
        pictureBox1.Image = item.Image;
    }

}