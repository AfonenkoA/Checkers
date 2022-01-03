using WinFormsClient.Model.Item;

namespace WinFormsClient.Control.Shop;

public partial class SoldCheckersShowPanel : UserControl
{
    private readonly ShopWindow _parent;
    private readonly int _id;
    internal SoldCheckersShowPanel(ShopWindow parent, VisualSoldItem item)
    {
        _parent = parent;
        _id = item.Id;
        InitializeComponent();
        TitleLabel.Text = item.Name;
        DescriptionLabel.Text = item.Detail;
        PriceLabel.Text = item.Price.ToString();
        Picture.Image = item.Image;

    }

    private  void BuyItemButton_Click(object sender, EventArgs e)
    {
        _parent.CheckersSkinId = _id;
        _parent.BuyCheckersSkin();
    }
}