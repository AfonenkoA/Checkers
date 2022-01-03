using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class MainMenuWindow : Form, IMainMenuView
{
    private readonly ApplicationContext _context;

    public MainMenuWindow(ApplicationContext context)
    {
        _context = context;
        InitializeComponent();
        ProfileButton.Click += (_, _) => Invoke(OnShowProfile);
        ShopButton.Click += (_,_)=>Invoke(OnShowShop);
        CollectionButton.Click +=(_, _) =>Invoke(OnShowCollection);
        PlayButton.Click += (_, _) => Invoke(OnShowGame);
        // LogOutButton.Click += (sender, args) => Invoke(LogOut);
    }

    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }
    public event Action OnShowProfile;
    public event Action OnShowShop;

    public event Action OnShowCollection;

    public event Action? OnShowGame;

    //public event Action LogOut;
    private void Invoke(Action action)
    {
        if (action != null) action();
    }

}