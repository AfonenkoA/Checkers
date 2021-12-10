using static DatabaseStartup.Declaration.Markup;

namespace DatabaseStartup.Filling.Entity;

public sealed class UserPictureArgs
{
    internal readonly string Login;
    internal readonly string Password;
    internal readonly string PicName;

    internal UserPictureArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw LineSplitException;
        Login = SqlString(strings[0]);
        Password = SqlString(strings[1]);
        PicName = SqlString(strings[2]);
    }
}