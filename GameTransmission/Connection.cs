using System.Net.Sockets;
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
        if (json == null) throw new ArgumentException("Can't read received string");
        var message = Deserialize<Message>(json);
        if (message == null) throw new ArgumentException("Message parse exception");
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