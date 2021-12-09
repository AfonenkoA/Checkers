namespace DatabaseStartup.Entity;

public sealed class FriendshipArgs
{
    internal readonly string Login;
    internal readonly string Friend;

    internal FriendshipArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;
        Login = CsvTable.SqlString(strings[0]);
        Friend = CsvTable.SqlString(strings[1]);
    }

}