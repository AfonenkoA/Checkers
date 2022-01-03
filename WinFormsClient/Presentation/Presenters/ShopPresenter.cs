using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;
using WinFormsClient.Service.Interface;

namespace WinFormsClient.Presentation.Presenters;

public class ShopPresenter : BasePresenter<IShopView, Credential>
{
    private Credential? _credential;
    private readonly IUserService _repository;

    public ShopPresenter(IApplicationController controller,
        IShopView view,
        IUserService repository) :
        base(controller, view)
    {
        _repository = repository;
        View.OnBackToMenu += BackToMenu;
        View.OnBuyCheckersSkin += BuyCheckersSkin;
        View.OnBuyAnimation += BuyAnimation;
        View.OnBuyLootBox += BuyLootBox;
    }
    public override void Run(Credential argument)
    {
        _credential = argument;
        Task.Run(UpdateShopInfo).Wait();
        View.Show();
    }
    private async Task UpdateShopInfo()
    {
        if (_credential == null) throw new ArgumentException("Null credential");
        var (_, shop) = await _repository.GetShop(_credential);
        View.SetShopInfo(shop);

    }
    private async void BuyCheckersSkin()
    {
        await _repository.BuyCheckersSkin(_credential, View.CheckersSkinId);
        await UpdateShopInfo().ConfigureAwait(true);
    }
    private async void BuyAnimation()
    {
        await _repository.BuyAnimation(_credential, View.AnimationId);
        await UpdateShopInfo().ConfigureAwait(true);
    }

    private async void BuyLootBox()
    {
        await _repository.BuyLootBox(_credential, View.LootBoxId);
        await UpdateShopInfo().ConfigureAwait(true);
    }

    private void BackToMenu()
    {
        if (_credential == null) throw new ArgumentException("Null credential");
        Controller.Run<MainMenuPresenter, Credential>(_credential);
        View.Close();
    }

}