namespace DatabaseStartup.Entity;

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
                      throw CsvTable.LineSplitException;
        Login = CsvTable.SqlString(strings[0]);
        Password = CsvTable.SqlString(strings[1]);
        Title = CsvTable.SqlString(strings[2]);
        Content = CsvTable.SqlString(strings[3]);
        File = strings[4];
    }

}