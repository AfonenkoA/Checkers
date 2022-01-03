using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IMainMenuView : IView
{
    event Action OnShowProfile;
    event Action OnShowShop;
    event Action OnShowCollection;
    event Action OnShowGame;
    //event Action LogOut;
}