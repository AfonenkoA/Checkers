namespace WinFormsClient.Presentation.Views;

internal interface IFriendWindow
{
    public int UserId { get; }
    public event Action OnShowFriend;
}