using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class LoginWindow : Form, ILoginView
{
    private readonly ApplicationContext _context;
    public LoginWindow(ApplicationContext context)
    {
        _context = context;
        InitializeComponent();

        EnterButton.Click += (_, _) => InvokeAction(OnLogIn);
    }

    public new void Show()
    {
        _context.MainForm = this;
        Application.Run(_context);
    }

    public string Login => LoginTextBox.Text;
    public string Password => PasswordTextBox.Text;

    public event Action? OnLogIn;

    public void ShowError(string errorMessage)
    {
        ErrorLabel.Text = errorMessage;
    }

    private static void InvokeAction(Action? action) => action?.Invoke();
}