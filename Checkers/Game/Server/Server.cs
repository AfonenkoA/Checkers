using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Checkers.Game.Server.Match;
using Checkers.Game.Server.Transmission;
using static System.Text.Json.JsonSerializer;
using EventArgs = System.EventArgs;

namespace Checkers.Game.Server;

public sealed class Server
{
    private readonly TcpListener _listener;
    public event EventHandler? OnStart;
    public event EventHandler? OnPlayerConnected;

    public Server(int port)
    {
        _listener = TcpListener.Create(port);
    }

    public async void Run()
    {
        OnStart?.Invoke(this, EventArgs.Empty);
        _listener.Start();
        var matchMaker = new MatchMaker();
        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();
            var stream = client.GetStream();
            var writer = new StreamWriter(stream) { AutoFlush = true };
            var reader = new StreamReader(stream);
            var message = await reader.ReadLineAsync();
            if (message != null)
            {
                var action = Deserialize<ConnectionAction>(message);
                if (action != null)
                {
                    OnPlayerConnected?.Invoke(this, EventArgs.Empty);
                    Player player = new(client, writer, reader);
                    var _ = Task.Run(player.Listen);
                    player.OnGameRequest += matchMaker.AddPlayer;
                }
            }
            else
            {
                await writer.DisposeAsync();
                reader.Dispose();
                client.Dispose();
            }
        }
        //_listener.Stop();
    }
}