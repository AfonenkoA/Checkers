using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using static System.Text.Json.JsonSerializer;
using EventArgs = System.EventArgs;

namespace Checkers.Server;

public sealed class MatchMaker
{
    private readonly Queue<Player> _players = new Queue<Player>();

    internal void AddPlayer(Player sender)
    {
        _players.Enqueue(sender);
        if (_players.Count < 2) return;
        var black = _players.Dequeue();
        var white = _players.Dequeue();
        var _ = Task.Run(new Match(black, white).Start);
    }

}

internal static class MatchExtensions
{
    public static void AddListener(this Player p, IGameController controller)
    {
        p.OnMove += controller.Move;
        p.OnEmote += controller.Emote;
        p.OnSurrender += controller.Surrender;
    }

    public static void RemoveListener(this Player p, IGameController controller)
    {
        p.OnMove -= controller.Move;
        p.OnEmote -= controller.Emote;
        p.OnSurrender -= controller.Surrender;
    }

    public static void AddListener(this GameModel model, Player p)
    {
        model.OnMove += p.SendEvent;
        model.OnEmote += p.SendEvent;
        model.OnGameStart += p.SendEvent;
        model.OnGameEnd += p.SendEvent;
    }

    public static void RemoveListener(this GameModel model, Player p)
    {
        model.OnMove -= p.SendEvent;
        model.OnEmote -= p.SendEvent;
        model.OnGameStart -= p.SendEvent;
        model.OnGameEnd -= p.SendEvent;
    }

    public static void AddListener(this GameModel model, Match m)
    {
        model.OnMove += m.Log;
        model.OnEmote += m.Log;
        model.OnGameStart += m.Log;
        model.OnGameEnd += m.Log;
    }

    public static void RemoveListener(this GameModel model, Match m)
    {
        model.OnMove -= m.Log;
        model.OnEmote -= m.Log;
        model.OnGameStart -= m.Log;
        model.OnGameEnd -= m.Log;
    }
}

class Match
{
    private readonly ServerGameModel _model;
    private readonly Player _black;
    private readonly Player _white;
    public Match(Player black, Player white)
    {
        _black = black;
        _white = white;
        _model = new ServerGameModel();
        black.AddListener(_model.BlackController);
        white.AddListener(_model.WhiteController);
        _model.AddListener(white);
        _model.AddListener(black);
        _model.AddListener(this);
    }

    public void Start() => Task.Run(_model.Run);


    public void Log(EmoteEvent e)
    {

    }
    public void Log(GameStartEvent e)
    {

    }

    public void Log(GameEndEvent e)
    {
        _model.RemoveListener(_black);
        _model.RemoveListener(_white);
        _model.RemoveListener(this);
    }

    public void Log(MoveEvent e)
    {

    }
}

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