using static DatabaseStartup.Declaration.Markup;

namespace DatabaseStartup.Filling.Entity;

public sealed class DirectedMessageArgs : MessageArgs
{
    internal readonly string Direction;
    internal DirectedMessageArgs(string line) : base(line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;
        Direction = SqlString(strings[3]);
    }
}