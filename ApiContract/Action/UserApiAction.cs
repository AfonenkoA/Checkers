namespace ApiContract.Action;

public sealed class UserApiAction : ApiAction
{
    private UserApiAction(string name) : base(name)
    { }

    public static readonly UserApiAction SelectCheckers = new(SelectCheckersValue);
    public static readonly UserApiAction SelectAnimation = new(SelectAnimationValue);
    public static readonly UserApiAction Authenticate = new(AuthenticateValue);
    public static readonly UserApiAction UpdateNick = new(UpdateNickValue);
    public static readonly UserApiAction UpdateLogin = new(UpdateLoginValue);
    public static readonly UserApiAction UpdatePassword = new(UpdatePasswordValue);
    public static readonly UserApiAction UpdateEmail = new(UpdateEmailValue);
    public static readonly UserApiAction AddFriend = new(AddFriendValue);
    public static readonly UserApiAction DeleteFriend = new(DeleteFriendValue);
    public static readonly UserApiAction AcceptFriend = new(AcceptFriendValue);
    public static readonly UserApiAction GetUsersByNick = new(GetUsersByNickValue);
    public static readonly UserApiAction UpdateUserPicture = new(UpdateUserPictureValue);
    public static readonly UserApiAction BuyAnimation = new(BuyAnimationValue);
    public static readonly UserApiAction BuyCheckersSkin = new(BuyCheckersSkinValue);
    public static readonly UserApiAction BuyLootBox = new(BuyLootBoxValue);

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
    public const string UpdateUserPictureValue = "update-picture";
    public const string BuyAnimationValue = "buy-animation";
    public const string BuyCheckersSkinValue = "buy-checkers-skin";
    public const string BuyLootBoxValue = "buy-loot-box";
}