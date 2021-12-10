using Common.Entity;
using DatabaseStartup.Filling.Entity;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static DatabaseStartup.Declaration.Markup;
using static DatabaseStartup.Filling.Common;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.MessageRepository;

namespace DatabaseStartup.Filling;


internal static class Friendship
{
    private const string FriendChatSource = "FriendsChat.csv";
    private const string FriendshipSource = "Friends.csv";

    public static readonly string State = $@"
GO
USE Checkers;
INSERT INTO {FriendshipStateTable}({FriendshipStateName}) VALUES
({SqlString((FriendshipState)1)}),
({SqlString((FriendshipState)2)}),
({SqlString((FriendshipState)3)});";

    private static string LoadFriends()
    {
        const string declaration = "GO\nDECLARE @id1 INT, @id2 INT\n";

        static string GetUserId(string var, string login) =>
            $"SET {var} = (SELECT {Id} FROM {UserTable} WHERE {Login} = {login});\n";

        static string CreateFriendship(FriendshipArgs f) =>
            GetUserId("@id1", f.Friend) +
            GetUserId("@id2", f.Login) +
            $"EXEC {CreateFriendshipProc} @id1, @id2";

        return declaration +
               string.Join('\n', ReadLines(DataFile(FriendshipSource))
                   .Select(s => CreateFriendship(new FriendshipArgs(s))));
    }

    private static string LoadFriendMessages()
    {
        const string declaration = $"GO\nDECLARE {IdVar} INT, @id1 INT, @id2 INT\n";

        static string SetUserId(string var, string login) =>
            $"SET {var} = (SELECT {Id} FROM {UserTable} WHERE {Login}={login});\n";

        static string SendMessage(DirectedMessageArgs m) =>
            SetUserId("@id1", m.Login) +
            SetUserId("@id2", m.Direction) +
            $"SET {IdVar} = (SELECT {ChatId} FROM {FriendshipTable} WHERE {User1Id}=@id1 AND {User2Id}=@id2)\n" +
            $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

        return declaration +
               string.Join('\n', ReadLines(DataFile(FriendChatSource))
                   .Select(s => SendMessage(new DirectedMessageArgs(s))));
    }

    public static readonly string Total = $@"
{LoadFriends()}
{LoadFriendMessages()}";
}