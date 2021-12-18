using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using WinFormsClient.Model.Item;

namespace WinFormsClient;

public partial class SoldLootboxShowPanel : UserControl
{
    private readonly IAsyncUserApi _userApi = new UserWebApi();
    private readonly VisualSoldItem _item;
    private readonly Credential _credential;
    public SoldLootboxShowPanel(VisualSoldItem item,Credential credential)
    {
        _credential = credential;
        _item = item;
        InitializeComponent();
        TitleLabel.Text = item.Name;
        DescriptionLabel.Text = item.Detail;
        PriceLabel.Text = item.Price.ToString();
        Picture.Image = item.Image;
    }

    private async void BuyItemButton_Click(object sender, EventArgs e)
    {
        await _userApi.BuyLootBox(_credential, _item.Id);
            
    }
}