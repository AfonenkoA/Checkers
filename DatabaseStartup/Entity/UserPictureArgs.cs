namespace DatabaseStartup.Entity;

public sealed class UserPictureArgs
{
    internal readonly string Login;
    internal readonly string Password;
    internal readonly string PicName;

    internal UserPictureArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;
        Login = CsvTable.SqlString(strings[0]);
        Password = CsvTable.SqlString(strings[1]);
        PicName = CsvTable.SqlString(strings[2]);
    }
}