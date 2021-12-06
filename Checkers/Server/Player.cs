using System;
using System.IO;
using System.Net.Sockets;
using static System.Text.Json.JsonSerializer;

namespace Checkers.Server;

public sealed class Player : IDisposable
{
    private readonly TcpClient _client;
    private readonly StreamWriter _writer;
    private readonly StreamReader _reader;

    public delegate void MoveActionHandler(MoveAction args);
    public delegate void EmoteActionHandler(EmoteAction args);
    public delegate void SurrenderHandler();
    public delegate void GameRequestHandler(Player sender);

    public event MoveActionHandler OnMove;
    public event EmoteActionHandler OnEmote;
    public event SurrenderHandler OnSurrender;
    public event GameRequestHandler OnGameRequest;

    public Player(TcpClient client, StreamWriter writer, StreamReader reader)
    {
        _client = client;
        _writer = writer;
        _reader = reader;
    }

    internal async void Listen()
    {
        while (true)
        {
            var json = await _reader.ReadLineAsync();
            if (json == null) continue;
            var action = Deserialize<Action>(json);
            if (action is { Type: nameof(ConnectionAction) })
            {
                var c = Deserialize<ConnectionAction>(json);
                await _writer.WriteLineAsync(Serialize(new ConnectionAcceptEvent { IsAccepted = true }));
            }
        }
    }

    public void SendEvent(EmoteEvent e)
    { }

    public void SendEvent(MoveEvent e)
    { }

    public void SendEvent(GameStartEvent e)
    { }

    public void SendEvent(GameEndEvent e)
    { }



    public void Dispose()
    {
        _client.Dispose();
    }
}