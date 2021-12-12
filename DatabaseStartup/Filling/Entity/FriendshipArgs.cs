using static DatabaseStartup.Declaration.Markup;
using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling.Entity;

public sealed class FriendshipArgs
{
    internal readonly string Login;
    internal readonly string Friend;

    internal FriendshipArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw LineSplitException;
        Login = SqlString(strings[0]);
        Friend = SqlString(strings[1]);
    }

}