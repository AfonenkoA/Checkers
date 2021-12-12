using Checkers.Data.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

public class MainMenuPresenter : BasePresenter<IMainMenuView,Credential>
{
    private Credential _credential;

    public MainMenuPresenter(IApplicationController controller, IMainMenuView view) : base(controller, view)
    {
       // View.ShowProfile += ShowProfile;
    }

    public override void Run(Credential argument)
    {
        _credential = argument;
        View.Show();
    }

    private void ShowProfile(Credential credential)
    {
        _credential = credential;

    }
}