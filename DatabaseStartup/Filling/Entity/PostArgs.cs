using static DatabaseStartup.Declaration.Markup;
using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling.Entity;

public sealed class PostArgs
{
    internal readonly string Login;
    internal readonly string Password;
    internal readonly string Title;
    internal readonly string Content;
    internal readonly string File;

    internal PostArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw LineSplitException;
        Login = SqlString(strings[0]);
        Password = SqlString(strings[1]);
        Title = SqlString(strings[2]);
        Content = SqlString(strings[3]);
        File = strings[4];
    }

}