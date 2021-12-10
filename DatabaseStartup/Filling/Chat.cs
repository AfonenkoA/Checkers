using Common.Entity;
using DatabaseStartup.Filling.Entity;
using static DatabaseStartup.Declaration.Markup;
using static WebService.Repository.MSSqlImplementation.ChatRepository;
using static WebService.Repository.MSSqlImplementation.Repository;
using static DatabaseStartup.Filling.Common;
using static WebService.Repository.MSSqlImplementation.MessageRepository;

namespace DatabaseStartup.Filling;

internal static class Chat
{
    private static readonly string Type = $@"
GO
USE Checkers;
INSERT INTO {ChatTypeTable}({ChatTypeName}) VALUES
({SqlString((ChatType)1)}),
({SqlString((ChatType)2)});";

    private static readonly string Public = $@"
GO
Use Checkers
EXEC {CreateChatProc} {SqlString(CommonChatName)},{SqlString(ChatType.Public)}";

    private const string CommonChatSource = "AllChat.csv";

    private static string LoadCommonChat()
    {
        const string declaration = $"GO\nDECLARE {IdVar} INT\n" +
                                   $"EXEC {IdVar} = {GetCommonChatIdProc}\n";

        static string SendMessage(MessageArgs m) =>
            $"EXEC {SendMessageProc} {m.Login}, {m.Password}, {IdVar}, {m.Content}";

        return declaration +
               string.Join('\n', ReadLines(DataFile(CommonChatSource))
                   .Select(s => SendMessage(new MessageArgs(s))));
    }

    public static readonly string CommonChat = $@"
{LoadCommonChat()}";

    public static readonly string Total = $@"
{Type}
{Public}";
}