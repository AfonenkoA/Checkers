using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface ILoginView : IView

{
    string Login { get; }
    string Password { get; }
    event Action LogIn;
    void ShowError(string errorMessage);
}