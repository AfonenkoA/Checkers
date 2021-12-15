using Api.Interface;
using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

public class ShopPresenter : BasePresenter<IShopView, Credential>
{
    private readonly IAsyncUserApi _userApi;
    private Credential? _credential;
    public ShopPresenter(IApplicationController controller, IShopView view, IAsyncUserApi userApi) :
        base(controller, view)
    {
        _userApi = userApi;
    }
    public override void Run(Credential argument)
    {
        _credential = argument;
        Task.Run(UpdateShopInfo).Wait();
        View.Show();
    }
    private async Task UpdateShopInfo()
    {
        var (_, user) = await _userApi.TryGetSelf(_credential ?? new Credential());
        var checkers = user.AvailableCheckersSkins;
        var animations = user.AvailableAnimations;
        var lootBoxes = user.AvailableLootBox;

        View.SetShopInfo(animations, lootBoxes, checkers);
    }
}