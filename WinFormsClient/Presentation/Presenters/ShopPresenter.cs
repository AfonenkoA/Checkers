using Api.Interface;
using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

public class ShopPresenter : BasePresenter<IShopView,Credential> 
{
    private readonly IAsyncUserApi _userApi;
    private readonly IAsyncItemApi _itemApi;
    private Credential _credential;
    public ShopPresenter(IApplicationController controller, IShopView view, IAsyncUserApi userApi,IAsyncItemApi itemApi) : base(controller, view)
    {
        _userApi = userApi;
        _itemApi = itemApi;
    }
    public override void Run(Credential argument)
    {
        _credential = argument;
        Task.Run(UpdateShopInfo).Wait();
        View.Show();
    }
    private async Task UpdateShopInfo()
    {
        var (_, user) = await _userApi.TryGetSelf(_credential);
        var checkers = user.AvailableCheckersSkins;
        var animations = user.AvailableAnimations;
        var lootboxes = user.AvailableLootBox;
        var l1 = new List<Control>();
        foreach (var i in checkers)
        {
            var (_, skin) = await _itemApi.TryGetCheckersSkin(i.Id);
            l1.Add(new ItemShowPanel(skin) );
        }
        var l2 = new List<Control>();
        foreach (var i in animations)
        {
            var (_, animation) = await _itemApi.TryGetAnimation(i.Id);
            l2.Add(new ItemShowPanel(animation));
        }
        var l3 = new List<Control>();
        foreach (var i in lootboxes)
        {
            var (_, lootbox) = await _itemApi.TryGetLootBox(i.Id);
            l3.Add(new ItemShowPanel(lootbox));
        }
        View.SetShopInfo(l1,l2,l3);
    }
}