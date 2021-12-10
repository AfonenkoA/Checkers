using static DatabaseStartup.Declaration.Markup;

namespace DatabaseStartup.Filling.Entity;

public sealed class ArticleArgs
{
    internal readonly string Login;
    internal readonly string Password;
    internal readonly string Title;
    internal readonly string Abstract;
    internal readonly string Content;
    internal readonly string File;

    internal ArticleArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;
        Login = SqlString(strings[0]);
        Password = SqlString(strings[1]);
        Title = SqlString(strings[2]);
        Abstract = SqlString(strings[3]);
        Content = SqlString(strings[4]);
        File = strings[5];
    }
}