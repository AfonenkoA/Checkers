using Api.Interface;
using Common.Entity;
using WinFormsClient.Model;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

public class ShopPresenter : BasePresenter<IShopView, Credential>
{
    private readonly IAsyncUserApi _userApi;
    private Credential? _credential;
    private readonly ResourceManager _manager;
    public ShopPresenter(IApplicationController controller,
        IShopView view,
        IAsyncUserApi userApi,
        ResourceManager manager) :
        base(controller, view)
    {
        _userApi = userApi;
        this._manager = manager;
        View.OnBackToMenu += BackToMenu;
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
        var checkers = user.AvailableCheckersSkins.ToList();
        var animations = user.AvailableAnimations.ToList();
        var lootBoxes = user.AvailableLootBox.ToList();

        await _manager.PreLoad(checkers);
        await _manager.PreLoad(animations);
        await _manager.PreLoad(lootBoxes);

        View.SetShopInfo(
            animations.Select(a => new VisualAnimation(a, _manager.Get(a))),
                checkers.Select(c => new VisualCheckersSkin(c, _manager.Get(c))),
                lootBoxes.Select(l => new VisualLootBox(l, _manager.Get(l))));

    }

    private void BackToMenu()
    {
        if (_credential == null) throw new ArgumentException("Empty credential");
        Controller.Run<MainMenuPresenter, Credential>(_credential);
        View.Close();
    }
}