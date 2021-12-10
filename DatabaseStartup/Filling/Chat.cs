using Common.Entity;
using static DatabaseStartup.Declaration.Markup;
using static WebService.Repository.MSSqlImplementation.ChatRepository;

namespace DatabaseStartup.Filling;

internal class Chat
{
    internal static readonly string Type = $@"
INSERT INTO {ChatTypeTable}({ChatTypeName}) VALUES
({SqlString((ChatType)1)}),
({SqlString((ChatType)2)});";

    internal static readonly string Public = $@"
GO
Use Checkers
EXEC {CreateChatProc} {SqlString(CommonChatName)},{SqlString(ChatType.Public)}";

}