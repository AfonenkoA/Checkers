using Common.Entity;
using WinFormsClient.Model.Item;

namespace WinFormsClient.Model;

public class User
{
    public Image Image { get; }
    public string Nick { get;}
    public int SocialCredit { get; }
    public DateTime LastActivity { get; }
    public IEnumerable<VisualAchievement> Achievements { get; }
    public VisualCheckersSkin SelectedCheckersSkin { get; }
    public VisualAnimation SelectedAnimation { get; }
    public User(BasicUserData user,
        IEnumerable<VisualAchievement> achievements,
        VisualCheckersSkin selectedCheckersSkin, 
        VisualAnimation selectedAnimation, 
        Image image)
    {
        Achievements = achievements;
        SelectedCheckersSkin = selectedCheckersSkin;
        SelectedAnimation = selectedAnimation;
        Image = image;
        Nick = user.Nick;
        LastActivity = user.LastActivity;
        SocialCredit = user.SocialCredit;
    }

    public User(User user)
    {
        Achievements = user.Achievements;
        SelectedCheckersSkin = user.SelectedCheckersSkin;
        SelectedAnimation = user.SelectedAnimation;
        Image = user.Image;
        Nick = user.Nick;
        LastActivity = user.LastActivity;
        SocialCredit = user.SocialCredit;
    }
}