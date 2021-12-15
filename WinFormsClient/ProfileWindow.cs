using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class ProfileWindow : Form, IProfileView
{
    private readonly ApplicationContext _context;
    public ProfileWindow(ApplicationContext context)
    {
        _context = context;
        InitializeComponent();
    }

    public new void Show()
    {
        
        base.Show();
    }

    public void SetUserInfo(string username, string password,string lastactivity)
    {
        NickLabel.Text = username;
        RatingLabel.Text = password;
        lastActivityLabel.Text= lastactivity;
    }
}