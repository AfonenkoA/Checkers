namespace Checkers.Api.Interface.Action;

public sealed class NewsApiAction : ApiAction
{
    private NewsApiAction(string name) : base(name) { }

    internal static readonly NewsApiAction UpdateArticleTitle = new(UpdateArticleTitleValue);
    internal static readonly NewsApiAction UpdateArticleAbstract = new(UpdateArticleAbstractValue);
    internal static readonly NewsApiAction UpdateArticleContent = new(UpdateArticleContentValue);
    internal static readonly NewsApiAction UpdateArticlePictureId = new(UpdateArticlePictureIdValue);

    public const string UpdateArticleTitleValue = "update-title";
    public const string UpdateArticleAbstractValue = "update-abstract";
    public const string UpdateArticleContentValue = "update-content";
    public const string UpdateArticlePictureIdValue = "update-picture";
}