using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using WinFormsClient.Model;

namespace WinFormsClient;

public partial class SoldCheckersShowPanel : UserControl
{
    private readonly IAsyncUserApi _userApi = new UserWebApi();
    private readonly VisualSoldItem _item;
    private readonly Credential _credential;
    public SoldCheckersShowPanel(VisualSoldItem item, Credential credential)
    {
        _credential = credential;
        _item = item;
        InitializeComponent();
        TitleLabel.Text = item.Name;
        DescriptionLabel.Text = item.Detail;
        PriceLabel.Text = item.Price.ToString();
        pictureBox1.Image = item.Image;

    }

    private async void BuyItemButton_Click(object sender, EventArgs e)
    {
        await _userApi.BuyCheckersSkin(_credential, _item.Id);
        Hide();
    }
}