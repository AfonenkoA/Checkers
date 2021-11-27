namespace Checkers.Api.Interface.Action;

public sealed class ForumApiAction : ApiAction
{
    private ForumApiAction(string name) : base(name) { }

    internal static readonly ForumApiAction UpdatePostTitle = new(UpdatePostTitleValue);
    internal static readonly ForumApiAction UpdatePostPicture = new(UpdatePostContentValue);
    internal static readonly ForumApiAction UpdatePostContent = new(UpdatePostPictureValue);

    public const string UpdatePostTitleValue = "update-title";
    public const string UpdatePostContentValue = "update-content";
    public const string UpdatePostPictureValue = "update-picture";
}