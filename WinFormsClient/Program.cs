using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Presenters;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
        
    public static readonly ApplicationContext Context = new ApplicationContext();

    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var controller = new ApplicationController(new LightInjectAdapder())
            .RegisterView<ILoginView, LoginWindow>()
            .RegisterView<IMainMenuView,MainMenuWindow>()
            .RegisterService<IAsyncUserApi, UserWebApi>()
            .RegisterInstance(new ApplicationContext());

        controller.Run<LoginPresenter>();
    }
}