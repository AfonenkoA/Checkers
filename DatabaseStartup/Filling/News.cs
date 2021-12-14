using DatabaseStartup.Filling.Entity;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;
using static WebService.Repository.MSSqlImplementation.NewsRepository;
using static WebService.Repository.MSSqlImplementation.MessageRepository;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static DatabaseStartup.Filling.Common;


namespace DatabaseStartup.Filling;

internal static class News
{

    private const string NewsSource = "News.csv";
    private const string NewsChatSource = "NewsChat.csv";

    private static string LoadNews()
    {
        static string SetPicture(string filename) =>
            $"EXEC {IdVar} = {CreateResourceFromFileProc} {ResourceFile(filename)}\n";
        static string CreateArticle(ArticleArgs a) =>
            SetPicture(a.File) +
            $"EXEC {CreateArticleProc} {a.Login}, {a.Password}, {a.Title}, {a.Abstract}, {a.Content}, {IdVar}";

        return DeclareId +
               string.Join('\n', ReadLines(DataFile(NewsSource))
                   .Select(s => CreateArticle(new ArticleArgs(s))));
    }


    private static string LoadNewsMessages()
    {
        static string GetPostId(string title) =>
            $"(SELECT {ArticlePostId} FROM {ArticleTable} WHERE {ArticleTitle}={title})";

        static string SetId(string title)
            => $"SET {IdVar} = (SELECT {ChatId} FROM {PostTable} WHERE {Id} = {GetPostId(title)});\n";

        static string CreateComment(DirectedMessageArgs m) =>
            SetId(m.Direction) +
            $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

        return DeclareId +
               string.Join('\n', ReadLines(DataFile(NewsChatSource))
                   .Select(s => CreateComment(new DirectedMessageArgs(s))));
    }

    public static readonly string Total = $@"
{LoadNews()}
{LoadNewsMessages()}";
}