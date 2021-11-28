namespace Checkers.Api.Interface.Action;

public sealed class UserApiAction : ApiAction
{
    private UserApiAction(string name) : base(name)
    { }

    internal static readonly UserApiAction SelectCheckers = new(SelectCheckersValue);
    internal static readonly UserApiAction SelectAnimation = new(SelectAnimationValue);
    internal static readonly UserApiAction Authenticate = new(AuthenticateValue);
    internal static readonly UserApiAction Buy = new(BuyValue);
    internal static readonly UserApiAction UpdateNick = new(UpdateNickValue);
    internal static readonly UserApiAction UpdateLogin = new(UpdateLoginValue);
    internal static readonly UserApiAction UpdatePassword = new(UpdatePasswordValue);
    internal static readonly UserApiAction UpdateEmail = new(UpdateEmailValue);
    internal static readonly UserApiAction AddFriend = new(AddFriendValue);
    internal static readonly UserApiAction DeleteFriend = new(DeleteFriendValue);
    internal static readonly UserApiAction AcceptFriend = new(AcceptFriendValue);
    internal static readonly UserApiAction GetUsersByNick = new(GetUsersByNickValue);

    //Values
    public const string SelectCheckersValue = "select-checkers";
    public const string SelectAnimationValue = "select-animation";
    public const string AuthenticateValue = "authenticate";
    public const string BuyValue = "buy";
    public const string UpdateNickValue = "update-nick";
    public const string UpdateLoginValue = "update-login";
    public const string UpdatePasswordValue = "update-password";
    public const string UpdateEmailValue = "update-email";
    public const string AddFriendValue = "add-friend";
    public const string DeleteFriendValue = "delete-friend";
    public const string AcceptFriendValue = "accept-friend";
    public const string GetUsersByNickValue = "get-by-nick";
}