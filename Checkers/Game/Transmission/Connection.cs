using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using static System.String;
using static Checkers.CommunicationProtocol;

namespace Checkers.Game.Transmission;


public sealed class Connection : IDisposable
{
    private readonly TcpClient _client;
    private readonly StreamWriter _writer;
    private readonly StreamReader _reader;

    internal Connection(TcpClient client)
    {
        _client = client;
        var stream = client.GetStream();
        _writer = new StreamWriter(stream) { AutoFlush = true };
        _reader = new StreamReader(stream);
    }

    public async Task<T?> ReceiveObject<T>() 
        => Deserialize<T>(await _reader.ReadLineAsync() ?? Empty);
    public Task Transmit<T>(T obj) => _writer.WriteLineAsync(Serialize(obj));
    public Task<string?> ReceiveString() => _reader.ReadLineAsync();

    public void Dispose()
    {
        _client.Dispose();
        _writer.Dispose();
        _reader.Dispose();
    }
}