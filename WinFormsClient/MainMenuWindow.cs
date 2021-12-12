using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class MainMenuWindow : Form, IMainMenuView
{
    private readonly ApplicationContext _context;

    public MainMenuWindow(ApplicationContext context)
    {
        _context = context;
        InitializeComponent();


    }
    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }
    public event Action ShowProfile;
    private void Invoke(Action action)
    {
        if (action != null) action();
    }
}