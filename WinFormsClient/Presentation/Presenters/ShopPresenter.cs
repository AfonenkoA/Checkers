using Api.Interface;
using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

public class ShopPresenter : BasePresenter<IShopView, Credential>
{
    private readonly IAsyncUserApi _userApi;
    private Credential? _credential;
    private readonly ResourceManager manager;
    public ShopPresenter(IApplicationController controller, IShopView view, IAsyncUserApi userApi) :
        base(controller, view)
    {
        _userApi = userApi;
        View.BackToMenu += BackToMenu;
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
        var l1 = new List<(CheckersSkin,Image)>();
        var checkersSkins = checkers.ToList();
        foreach (var i in checkersSkins)
            l1.Add((i,await manager.Get(i.Resource.Id)));
        var l2 = new List<(Animation, Image)>();
        var animationsSkins = animations.ToList();
        foreach (var i in animationsSkins)
            l2.Add((i, await manager.Get(i.Resource.Id)));
        var l3 = new List<(LootBox, Image)>();
        var lootBoxesSkins = lootBoxes.ToList();
        foreach (var i in lootBoxesSkins)
            l3.Add((i, await manager.Get(i.Resource.Id)));

        View.SetShopInfo(l1,l2,l3);
    }

    private void BackToMenu()
    {
        Controller.Run<MainMenuPresenter,Credential>(_credential);
        View.Close();
    }
}