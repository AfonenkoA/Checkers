using static DatabaseStartup.Declaration.Markup;
using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling.Entity;

internal class MessageArgs
{
    internal readonly string Login;
    internal readonly string Password;
    internal readonly string Content;

    internal MessageArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw LineSplitException;
        Login = SqlString(strings[0]);
        Password = SqlString(strings[1]);
        Content = SqlString(strings[2]);
    }
}