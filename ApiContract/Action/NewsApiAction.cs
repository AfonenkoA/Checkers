namespace ApiContract.Action;

public sealed class NewsApiAction : ApiAction
{
    private NewsApiAction(string name) : base(name) { }

    public static readonly NewsApiAction UpdateArticleTitle = new(UpdateArticleTitleValue);
    public static readonly NewsApiAction UpdateArticleAbstract = new(UpdateArticleAbstractValue);
    public static readonly NewsApiAction UpdateArticleContent = new(UpdateArticleContentValue);
    public static readonly NewsApiAction UpdateArticlePictureId = new(UpdateArticlePictureIdValue);

    public const string UpdateArticleTitleValue = "update-title";
    public const string UpdateArticleAbstractValue = "update-abstract";
    public const string UpdateArticleContentValue = "update-content";
    public const string UpdateArticlePictureIdValue = "update-picture";
}