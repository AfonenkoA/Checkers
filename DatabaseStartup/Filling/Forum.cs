using DatabaseStartup.Filling.Entity;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.MessageRepository;
using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling;

internal static class Forum
{
    private const string PostSource = "ForumPosts.csv";
    private const string ForumChatSource = "ForumChat.csv";

    private static string LoadPosts()
    {
        static string SetPicture(string filename) =>
            $"EXEC {IdVar} = {CreateResourceFromFileProc} {ResourceFile(filename)}\n";

        static string CreatePost(PostArgs p) =>
            SetPicture(p.File) +
            $"EXEC {CreatePostProc} {p.Login}, {p.Password}, {p.Title}, {p.Content}, {IdVar}";

        return DeclareId +
               string.Join('\n', ReadLines(DataFile(PostSource))
                   .Select(s => CreatePost(new PostArgs(s))));
    }

    private static string LoadPostMessages()
    {
        static string GetChatId(string title) =>
            $"(SELECT {ChatId} FROM {PostTable} WHERE {PostTitle}={title});\n";

        static string CreateComment(DirectedMessageArgs m) =>
            $"SET {IdVar} = {GetChatId(m.Direction)}" +
            $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

        return DeclareId +
               string.Join('\n', ReadLines(DataFile(ForumChatSource))
                   .Select(s => CreateComment(new DirectedMessageArgs(s))));
    }

    public static readonly string Total = $@"
{LoadPosts()}
{LoadPostMessages()}";
}