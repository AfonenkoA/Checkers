using WinFormsClient.Model;

namespace WinFormsClient;

public sealed partial class SelectedItemShowPanel : UserControl
{
    private readonly CollectionWindow _parent;
    private readonly int _id;
    public SelectedItemShowPanel(CollectionWindow parent, VisualDetailedItem item)
    {
        _parent = parent;
        _id = item.Id;
        InitializeComponent();
        BackColor = Color.Blue;
        TitleLabel.Text = item.Name;
        DescriptionLabel.Text = item.Detail;
        pictureBox1.Image = item.Image;
    }

    private void SelectItemButton_Click(object sender, EventArgs e)
    {
        _parent.SelectedAnimationsId = _id;
    }
}