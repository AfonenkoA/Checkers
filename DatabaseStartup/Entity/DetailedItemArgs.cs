namespace DatabaseStartup.Entity;

public class DetailedItemArgs : NamedItemArgs
{
    private readonly string _detail;
    internal DetailedItemArgs(string line) : base(line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;

        _detail = CsvTable.SqlString(strings[2]);
    }

    public override string ToString() => base.ToString() + $", {_detail}";
}