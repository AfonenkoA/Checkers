﻿using static DatabaseStartup.Declaration.Markup;
using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling.Entity;

internal sealed class UserArgs
{
    private readonly string _nick;
    private readonly string _login;
    private readonly string _password;
    private readonly string _email;
    private readonly string _type;

    internal UserArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw LineSplitException;
        _nick = SqlString(strings[0]);
        _login = SqlString(strings[1]);
        _password = SqlString(strings[2]);
        _email = SqlString(strings[3]);
        _type = SqlString(strings[4]);
    }

    public override string ToString() =>
        $"{_nick}, {_login}, {_password}, {_email}, {_type}";
}