using WinFormsClient.Model.Item;
using WinFormsClient.Model.Item.Collection;
using static System.Drawing.Color;

namespace WinFormsClient.Control.Collection;

internal sealed partial class CollectionAnimationPanel : UserControl
{
    private readonly CollectionWindow _parent;
    private readonly int _id;

    internal CollectionAnimationPanel(CollectionWindow parent, CollectionAnimation animation)
    {
        _parent = parent;
        _id = animation.Id;
        InitializeComponent();
        if (animation.IsSelected)
            BackColor = GreenYellow;
        TitleLabel.Text = animation.Name;
        DescriptionLabel.Text = animation.Detail;
        Picture.Image = animation.Image;
    }

    private void SelectItemButton_Click(object sender, EventArgs e)
    {
        _parent.SelectedAnimationsId = _id;
        _parent.SelectAnimation();
    }
}