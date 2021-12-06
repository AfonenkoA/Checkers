using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Checkers.Client;
using Checkers.Data.Entity;
using static System.Text.Json.JsonSerializer;

namespace Checkers.Server;

public sealed class Client : IDisposable
{
    private readonly TcpClient _client;
    private readonly StreamWriter _writer;
    private readonly StreamReader _reader;

    public Client(int port)
    {
        _client = new TcpClient();
        _client.Connect(IPAddress.Loopback, port);
        var stream = _client.GetStream();
        _writer = new StreamWriter(stream) { AutoFlush = true };
        _reader = new StreamReader(stream);
    }

    public event EventHandler<ConnectionAcceptEvent?>? OnConnect;

    public async void Listen()
    {
        while (true)
        {
            var json = await _reader.ReadLineAsync();
            if (json == null) continue;
            var message = Deserialize<Action>(json);
            if (message is { Type: nameof(ConnectionAcceptEvent) })
            {
                var c = Deserialize<ConnectionAcceptEvent>(json);
                OnConnect?.Invoke(this, c);
            }
        }
    }

    public Task Send() =>
        _writer.WriteLineAsync(Serialize(new ConnectionAction { Credential = new Credential { Login = "log", Password = "pass" } }));


    public ClientGameModel Play()
    {
        return new ClientGameModel(Color.Black);
    }

    public void Dispose()
    {
        _client.Dispose();
        _writer.Dispose();
        _reader.Dispose();
    }
}