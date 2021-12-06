namespace Checkers.Game.Server.Transmission;

public class Message
{
    public string Type { get; set; } = string.Empty;
    protected Message(string type) => Type = type;
    public Message() { }
}