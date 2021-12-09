namespace DatabaseStartup.Entity;

public class MessageArgs
{
    internal readonly string Login;
    internal readonly string Password;
    internal readonly string Content;

    internal MessageArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;
        Login = CsvTable.SqlString(strings[0]);
        Password = CsvTable.SqlString(strings[1]);
        Content = CsvTable.SqlString(strings[2]);
    }
}