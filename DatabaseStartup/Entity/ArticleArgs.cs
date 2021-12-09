namespace DatabaseStartup.Entity;

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
        Login = CsvTable.SqlString(strings[0]);
        Password = CsvTable.SqlString(strings[1]);
        Title = CsvTable.SqlString(strings[2]);
        Abstract = CsvTable.SqlString(strings[3]);
        Content = CsvTable.SqlString(strings[4]);
        File = strings[5];
    }
}