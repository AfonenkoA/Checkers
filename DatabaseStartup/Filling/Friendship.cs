using Common.Entity;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static DatabaseStartup.Declaration.Markup;

namespace DatabaseStartup.Filling;


internal class Friendship
{
    internal static readonly string State = $@"
INSERT INTO {FriendshipStateTable}({FriendshipStateName}) VALUES
({SqlString((FriendshipState)1)}),
({SqlString((FriendshipState)2)}),
({SqlString((FriendshipState)3)});";
}