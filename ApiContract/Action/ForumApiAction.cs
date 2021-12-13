namespace ApiContract.Action;

public sealed class ForumApiAction : ApiAction
{
    private ForumApiAction(string name) : base(name) { }

    public static readonly ForumApiAction UpdatePostTitle = new(UpdatePostTitleValue);
    public static readonly ForumApiAction UpdatePostPicture = new(UpdatePostPictureValue);
    public static readonly ForumApiAction UpdatePostContent = new(UpdatePostContentValue);

    public const string UpdatePostTitleValue = "update-title";
    public const string UpdatePostContentValue = "update-content";
    public const string UpdatePostPictureValue = "update-picture";
}