using WinFormsClient.Model;

namespace WinFormsClient.Control;

public partial class UserInfoPanel : UserControl
{
    private readonly FriendsWindow _parent;
    public UserInfoPanel(FriendsWindow parent, User user)
    {
        _parent = parent;
        InitializeComponent();
        NickButton.Text = user.Nick;
        SocialCreditLabel.Text = user.SocialCredit.ToString();
    }
}