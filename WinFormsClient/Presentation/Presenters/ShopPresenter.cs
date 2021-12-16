using Api.Interface;
using Common.Entity;
using WinFormsClient.Model;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;
using WinFormsClient.Repository.Implementation;
using WinFormsClient.Repository.Interface;

namespace WinFormsClient.Presentation.Presenters;

public class ShopPresenter : BasePresenter<IShopView, Credential>
{
    private Credential? _credential;
    private readonly IUserRepository _repository;

    public ShopPresenter(IApplicationController controller,
        IShopView view,
        IUserRepository repository) :
        base(controller, view)
    {
        _repository = repository;
        View.OnBackToMenu += BackToMenu;
        View.ReloadShop += ReloadShop;
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
        var (_, user) = await _repository.GetSelf(_credential);

        View.SetShopInfo(_credential,
            user.AvailableAnimations,
                user.AvailableCheckersSkins,
                user.AvailableLootBoxes);

    }

    private void BackToMenu()
    {
        if (_credential == null) throw new ArgumentException("Null credential");
        Controller.Run<MainMenuPresenter, Credential>(_credential);
        View.Close();
    }

    private void ReloadShop()
    {
        Controller.Run<ShopPresenter, Credential>(_credential);
        View.Close();
    }
}