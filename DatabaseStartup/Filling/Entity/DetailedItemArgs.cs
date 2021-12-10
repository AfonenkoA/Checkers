using static DatabaseStartup.Declaration.Markup;
using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling.Entity;

public class DetailedItemArgs : NamedItemArgs
{
    private readonly string _detail;
    internal DetailedItemArgs(string line) : base(line)
    {
        var strings = line.Split(";") ??
                      throw LineSplitException;

        _detail = SqlString(strings[2]);
    }

    public override string ToString() => base.ToString() + $", {_detail}";
}