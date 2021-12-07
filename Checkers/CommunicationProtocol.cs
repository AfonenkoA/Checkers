using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Checkers;

public static class CommunicationProtocol
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };
    public static T? Deserialize<T>(string s) => JsonSerializer.Deserialize<T>(s, Options);
    public static string Serialize<T>(T obj) => JsonSerializer.Serialize(obj,Options);
}