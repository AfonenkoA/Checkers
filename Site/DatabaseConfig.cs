using static System.String;

namespace Checkers.Site;

public sealed class DatabaseConfig
{
    public string Old { get; set; } = Empty;
    public string Current { get; set; } = Empty;
}