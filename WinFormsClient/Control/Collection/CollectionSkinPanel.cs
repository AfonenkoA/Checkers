using WinFormsClient.Model.Item;
using static System.Drawing.Color;

namespace WinFormsClient.Control.Collection;

public sealed partial class CollectionSkinPanel : UserControl
{
    private readonly CollectionWindow _parent;
    private readonly int _id;

    internal CollectionSkinPanel(CollectionWindow parent, CollectionCheckersSkin skin)
    {
        _parent = parent;
        _id = skin.Id;
        InitializeComponent();
        if (skin.IsSelected)
            BackColor = GreenYellow;
        TitleLabel.Text = skin.Name;
        DescriptionLabel.Text = skin.Detail;
        Picture.Image = skin.Image;
    }

    private void SelectItemButton_Click(object sender, EventArgs e)
    {
        _parent.SelectedCheckersId = _id;
        _parent.SelectCheckersSkin();
    }
}