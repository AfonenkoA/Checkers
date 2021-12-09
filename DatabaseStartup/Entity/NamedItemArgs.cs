namespace DatabaseStartup.Entity;

public class NamedItemArgs
{
    private readonly string _name;
    private readonly string _path;

    internal NamedItemArgs(string line)
    {
        var strings = line.Split(";") ??
                      throw CsvTable.LineSplitException;

        _name = CsvTable.SqlString(strings[0]);
        _path = CsvTable.ResourceFile(strings[1]);
    }

    public override string ToString() => $"{_name}, {_path}";
}