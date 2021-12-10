using static DatabaseStartup.Declaration.Markup;
using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling.Entity;

public sealed class DirectedMessageArgs : MessageArgs
{
    internal readonly string Direction;
    internal DirectedMessageArgs(string line) : base(line)
    {
        var strings = line.Split(";") ??
                      throw LineSplitException;
        Direction = SqlString(strings[3]);
    }
}