namespace DatabaseStartup.Entity;

public sealed class DirectedMessageArgs : MessageArgs
{
    internal readonly string Direction;
    internal DirectedMessageArgs(string line) : base(line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;
        Direction = CsvTable.SqlString(strings[3]);
    }
}