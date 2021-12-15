using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

public class MainMenuPresenter : BasePresenter<IMainMenuView,Credential>
{
    private Credential _credential;

    public MainMenuPresenter(IApplicationController controller, IMainMenuView view) : base(controller, view)
    {
        View.ShowProfile += ShowProfile;
        View.ShowShop += ShowShop;
        View.ShowCollection += ShowCollection;
        //View.LogOut += LogOut;
    }

    public override void Run(Credential argument)
    {
        _credential = argument;
        View.Show();
    }

    private void ShowProfile()
    {
        
        Controller.Run<ProfilePresenter,Credential>(_credential);
        
    }

    private void ShowShop()
    {
        Controller.Run<ShopPresenter,Credential>(_credential);
        View.Close();
    }

    private void ShowCollection()
    {
        Controller.Run<CollectionPresenter,Credential>(_credential);
    }

    private void LogOut()
    {
        Controller.Run<LoginPresenter>();
    }
}