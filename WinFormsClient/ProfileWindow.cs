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

    public void SetUserInfo(string username, string socialCredit,string lastActivity,Image avatar)
    {
        NickLabel.Text = username;
        RatingLabel.Text = socialCredit;
        lastActivityLabel.Text= lastActivity;
        avatarPictureBox.Image = avatar;
    }
}