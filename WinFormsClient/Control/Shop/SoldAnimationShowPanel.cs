using Api.Interface;
using Api.WebImplementation;
using WinFormsClient.Model.Item;

namespace WinFormsClient.Control.Shop;

public sealed partial class SoldAnimationShowPanel : UserControl
{
    private readonly IAsyncUserApi _userApi = new UserWebApi();
    private readonly ShopWindow _parent;
    private readonly int _id;
    public SoldAnimationShowPanel(ShopWindow parent,VisualSoldItem item)
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

        _parent.AnimationId = _id;
        _parent.BuyAnimation();

    }
}