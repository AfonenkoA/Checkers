namespace DatabaseStartup.Entity;

public sealed class UserArgs
{
    private readonly string _nick;
    private readonly string _login;
    private readonly string _password;
    private readonly string _email;
    private readonly string _type;

    internal UserArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;
        _nick = CsvTable.SqlString(strings[0]);
        _login = CsvTable.SqlString(strings[1]);
        _password = CsvTable.SqlString(strings[2]);
        _email = CsvTable.SqlString(strings[3]);
        _type = CsvTable.SqlString(strings[4]);
    }

    public override string ToString() =>
        $"{_nick}, {_login}, {_password}, {_email}, {_type}";
}