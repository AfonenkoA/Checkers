using System.Text.Json.Serialization;
using static System.String;
using static Common.CommunicationProtocol;

namespace GameTransmission;

public sealed class Message
{
    public string Type { get; set; } = Empty;
    public string Value { get; set; } = Empty;

    [JsonConstructor]
    public Message() { }

    public T GetAs<T>()
    {
        if (Type != typeof(T).Name) throw new ArgumentException();
        var val = Deserialize<T>(Value);
        if (val == null) throw new ArgumentException();
        return val;
    }

    internal static string FromValue<T>(T val) =>
        Serialize(new Message
        {
            Type = typeof(T).Name,
            Value = Serialize(val)
        });

    public static Message? FromString(string s) =>
        Deserialize<Message>(s);
}
