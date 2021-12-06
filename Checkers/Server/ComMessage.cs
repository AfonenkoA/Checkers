namespace Checkers.Server;

public class ComMessage
{
    public string Type { get; set; } = string.Empty;
    protected ComMessage(string type) => Type = type;
    public ComMessage() { }
}