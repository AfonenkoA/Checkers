using Api.Interface;
using Api.WebImplementation;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Presenters;
using WinFormsClient.Presentation.Views;
using WinFormsClient.Service.Implementation;
using WinFormsClient.Service.Interface;

namespace WinFormsClient;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>

    public static readonly ApplicationContext Context = new();

    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var controller = new ApplicationController(new LightInjectAdapder())
            .RegisterView<ILoginView, LoginWindow>()
            .RegisterView<IMainMenuView, MainMenuWindow>()
            .RegisterView<IProfileView, ProfileWindow>()
            .RegisterView<IShopView, ShopWindow>()
            .RegisterView<ICollectionView, CollectionWindow>()
            .RegisterService<IAsyncUserApi, UserWebApi>()
            .RegisterService<IAsyncItemApi, ItemWebApi>()
            .RegisterService<IAsyncResourceService, AsyncResourceWebApi>()
            .RegisterService<IResourceService, ResourceManager>()
            .RegisterService<IItemService, ItemService>()
            .RegisterService<IUserService, UserService>()
            .RegisterService<IGameView, GameWindow>()
            .RegisterInstance(Context);

        controller.Run<LoginPresenter>();
    }
}