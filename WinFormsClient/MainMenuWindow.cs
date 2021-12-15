using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class MainMenuWindow : Form, IMainMenuView
{
    private readonly ApplicationContext _context;

    public MainMenuWindow(ApplicationContext context)
    {
        _context = context;
        InitializeComponent();
        ProfileButton.Click += (sender, args) => Invoke(ShowProfile);
        ShopButton.Click += (sender,args)=>Invoke(ShowShop);
       // LogOutButton.Click += (sender, args) => Invoke(LogOut);
    }

   

    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }
    public event Action ShowProfile;
    public event Action ShowShop;

    public event Action ShowCollection;
    //public event Action LogOut;
    private void Invoke(Action action)
    {
        if (action != null) action();
    }
}