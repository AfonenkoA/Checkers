using DatabaseStartup.Filling.Entity;
using static System.IO.File;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.ForumRepository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.MessageRepository;
using static WebService.Repository.MSSqlImplementation.NewsRepository;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ResourceRepository;
using static WebService.Repository.MSSqlImplementation.UserRepository;

namespace DatabaseStartup.Filling;

internal static class CsvTable
{


    public const string PictureSource = "AvatarPicture.csv";
    public const string CheckersSource = "CheckersSkins.csv";
    public const string AnimationSource = "Animations.csv";
    public const string AchievementsSource = "Achivements.csv";
    public const string LootBoxSource = "LootBoxes.csv";
    public const string UserSource = "Users.csv";

    public const string NewsSource = "News.csv";
    public const string PostSource = "ForumPosts.csv";
    public const string NewsChatSource = "NewsChat.csv";
    public const string ForumChatSource = "ForumChat.csv";
    public const string FriendChatSource = "FriendsChat.csv";
    public const string FriendshipSource = "Friends.csv";
    public const string CommonChatSource = "AllChat.csv";



    public static string LoadPictures()=>
        string.Join('\n',ReadLines(DataFile(PictureSource))
            .Select(s=>Exec(CreatePictureProc,new NamedItemArgs(s))));

    public static string LoadAchievements() =>
        string.Join('\n', ReadLines(DataFile(AchievementsSource))
            .Select(s => Exec(CreateAchievementProc, new DetailedItemArgs(s))));

    public static string LoadAnimations() =>
        string.Join('\n', ReadLines(DataFile(AnimationSource))
            .Select(s => Exec(CreateAnimationProc, new SoldItemArgs(s))));

    public static string LoadLootBoxes() =>
        string.Join('\n', ReadLines(DataFile(LootBoxSource))
            .Select(s => Exec(CreateLootBoxProc, new SoldItemArgs(s))));

    public static string LoadCheckersSkins() =>
        string.Join('\n', ReadLines(DataFile(CheckersSource))
            .Select(s => Exec(CreateCheckersSkinProc, new SoldItemArgs(s))));

    public static string LoadUsers() =>
        string.Join('\n', ReadLines(DataFile(UserSource))
            .Select(s => new UserArgs(s))
            .Select(i => Exec(CreateUserProc, i)));



    public static string LoadNews()
    {
        static string SetPicture(string filename) =>
            $"EXEC {IdVar} = {CreateResourceFromFileProc} {ResourceFile(filename)}\n";
        static string CreateArticle(ArticleArgs a) =>
            SetPicture(a.File) +
            $"EXEC {CreateArticleProc} {a.Login}, {a.Password}, {a.Title}, {a.Abstract}, {a.Content}, {IdVar}";

        return Declaration +
               string.Join('\n', ReadLines(DataFile(NewsSource))
                   .Select(s => CreateArticle(new ArticleArgs(s))));
    }

    public static string LoadPosts()
    {
        static string SetPicture(string filename) =>
            $"EXEC {IdVar} = {CreateResourceFromFileProc} {ResourceFile(filename)}\n";

        static string CreatePost(PostArgs p) =>
            SetPicture(p.File) +
            $"EXEC {CreatePostProc} {p.Login}, {p.Password}, {p.Title}, {p.Content}, {IdVar}";

        return Declaration +
               string.Join('\n', ReadLines(DataFile(PostSource))
                   .Select(s=>CreatePost(new PostArgs(s))));
    }

    public static string LoadNewsMessages()
    {
        static string GetPostId(string title) =>
            $"(SELECT {ArticlePostId} FROM {ArticleTable} WHERE {ArticleTitle}={title})";
        
        static string SetId(string title)
            => $"SET {IdVar} = (SELECT {ChatId} FROM {PostTable} WHERE {Id} = {GetPostId(title)});\n";

        static string CreateComment(DirectedMessageArgs m) =>
            SetId(m.Direction) +
            $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

        return Declaration +
               string.Join('\n', ReadLines(DataFile(NewsChatSource))
                   .Select(s => CreateComment(new DirectedMessageArgs(s))));
    }

    public static string LoadPostMessages()
    {
        static string GetChatId(string title) =>
            $"(SELECT {ChatId} FROM {PostTable} WHERE {PostTitle}={title});\n";

        static string CreateComment(DirectedMessageArgs m) =>
            $"SET {IdVar} = {GetChatId(m.Direction)}" +
            $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

        return Declaration +
               string.Join('\n', ReadLines(DataFile(ForumChatSource))
                   .Select(s => CreateComment(new DirectedMessageArgs(s))));
    }

    public static string LoadFriends()
    {
        const string declaration = "DECLARE @id1 INT, @id2 INT\n";

        static string GetUserId(string var,string login) =>
            $"SET {var} = (SELECT {Id} FROM {UserTable} WHERE {Login} = {login});\n";

        static string CreateFriendship(FriendshipArgs f) =>
            GetUserId("@id1",f.Friend)+
            GetUserId("@id2",f.Login)+
            $"EXEC {CreateFriendshipProc} @id1, @id2";

        return declaration +
               string.Join('\n', ReadLines(DataFile(FriendshipSource))
                   .Select(s => CreateFriendship(new FriendshipArgs(s))));
    }

    public static string LoadFriendMessages()
    {
        const string declaration = $"DECLARE {IdVar} INT, @id1 INT, @id2 INT\n";

        static string SetUserId(string var, string login) =>
            $"SET {var} = (SELECT {Id} FROM {UserTable} WHERE {Login}={login});\n";
        
        static string SendMessage(DirectedMessageArgs m) =>
            SetUserId("@id1",m.Login) +
            SetUserId("@id2",m.Direction) +
            $"SET {IdVar} = (SELECT {ChatId} FROM {FriendshipTable} WHERE {User1Id}=@id1 AND {User2Id}=@id2)\n"+
            $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

        return declaration +
               string.Join('\n', ReadLines(DataFile(FriendChatSource))
                   .Select(s => SendMessage(new DirectedMessageArgs(s))));
    }

    public static string LoadCommonChat()
    {
        const string declaration = $"DECLARE {IdVar} INT\n" +
                                   $"EXEC {IdVar} = {GetCommonChatIdProc}\n";

        static string SendMessage(MessageArgs m) =>
            $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

        return declaration +
               string.Join('\n', ReadLines(DataFile(CommonChatSource))
                   .Select(s => SendMessage(new MessageArgs(s))));
    }
}