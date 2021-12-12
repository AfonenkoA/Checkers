using System.Net.Sockets;
using static System.String;
using static Common.CommunicationProtocol;

namespace GameTransmission;


public sealed class Connection : IDisposable
{
    public const int ServerPort = 6302;
    private readonly TcpClient _client;
    private readonly StreamWriter _writer;
    private readonly StreamReader _reader;

    public Connection(TcpClient client)
    {
        _client = client;
        var stream = client.GetStream();
        _writer = new StreamWriter(stream) { AutoFlush = true };
        _reader = new StreamReader(stream);
    }

    public async Task<T?> ReceiveObject<T>()
    {
        var json = await _reader.ReadLineAsync();
        var message = Deserialize<Message>(json);
        return message.GetAs<T>();
    }

    public Task Transmit<T>(T obj)
    {
        var message = Message.FromValue(obj);
        return _writer.WriteLineAsync(message);
    }

    public Task<string?> ReceiveString() => _reader.ReadLineAsync();

    public void Dispose()
    {
        _client.Dispose();
        _writer.Dispose();
        _reader.Dispose();
    }
}