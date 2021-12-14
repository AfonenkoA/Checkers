using static DatabaseStartup.Declaration.Markup;
using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling.Entity;

internal class NamedItemArgs
{
    private readonly string _name;
    private readonly string _path;

    internal NamedItemArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw LineSplitException;

        _name = SqlString(strings[0]);
        _path = ResourceFile(strings[1]);
    }

    public override string ToString() => $"{_name}, {_path}";
}