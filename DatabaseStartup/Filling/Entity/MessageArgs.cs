using static DatabaseStartup.Declaration.Markup;

namespace DatabaseStartup.Filling.Entity;

public class MessageArgs
{
    internal readonly string Login;
    internal readonly string Password;
    internal readonly string Content;

    internal MessageArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;
        Login = SqlString(strings[0]);
        Password = SqlString(strings[1]);
        Content = SqlString(strings[2]);
    }
}