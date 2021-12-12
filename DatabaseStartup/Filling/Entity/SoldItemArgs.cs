using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling.Entity;

public sealed class SoldItemArgs : DetailedItemArgs
{
    private readonly string _price;
    internal SoldItemArgs(string line) : base(line)
    {
        var strings = line.Split(";") ??
                      throw LineSplitException;

        _price = strings[3];
    }

    public override string ToString() => base.ToString() + $", {_price}";
}