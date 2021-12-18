using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

internal interface ILoginView : IView
{
    string Login { get; }
    string Password { get; }
    event Action OnLogIn;
    void ShowError(string errorMessage);
}