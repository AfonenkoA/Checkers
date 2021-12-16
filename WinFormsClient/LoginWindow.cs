using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class LoginWindow : Form, ILoginView
{
    private readonly ApplicationContext _context;
    public LoginWindow(ApplicationContext context)
    {
        _context = context;
        InitializeComponent();

        EnterButton.Click += (sender, args) => Invoke(LogIn);
    }

    public new void Show()
    {
        _context.MainForm = this;
        Application.Run(_context);
    }

    public string Login { get { return LoginTextBox.Text; } }
    public string Password { get { return PasswordTextBox.Text; } }
    public event Action LogIn;

    public void ShowError(string errorMessage)
    {
        ErrorLabel.Text = errorMessage;
    }

    private void Invoke(Action action)
    {
        if (action != null) action();
    }
}