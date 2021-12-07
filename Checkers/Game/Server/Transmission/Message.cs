namespace Checkers.Game.Server.Transmission;

public class Message
{
    internal string Type { get; set; } = string.Empty;
    protected Message(string type) => Type = type;
    internal Message() { }
}