using Checkers.Data.Entity;
using Checkers.Game.Transmission;
using System;
using static Checkers.CommunicationProtocol;
using Action = Checkers.Game.Transmission.Action;

namespace Checkers.Game.Server;

public sealed class Player : IDisposable
{
    private readonly Connection _connection;
    internal Credential Credential { get; }

    public delegate void MoveActionHandler(int from, int to);
    public delegate void EmoteActionHandler(int emotionId);
    public delegate void SurrenderHandler();

    public delegate void GameRequestHandler(Player sender);
    public delegate void DisconnectActionHandler(Player sender);

    public event MoveActionHandler? OnMove;
    public event EmoteActionHandler? OnEmote;
    public event SurrenderHandler? OnSurrender;
    public event GameRequestHandler? OnGameRequest;
    public event DisconnectActionHandler? OnDisconnect;

    public Player(Connection connection, Credential credential)
    {
        _connection = connection;
        Credential = credential;
    }

    internal async void Listen()
    {
        while (true)
        {
            var json = await _connection.ReceiveString();
            if (json == null) continue;
            var action = Deserialize<Action>(json);
            if (action == null) continue;
            switch (action.Type)
            {
                case nameof(GameRequestAction):
                    Deserialize<GameRequestAction>(json);
                    break;
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

    public void SendEvent(ConnectAcknowledgeEvent e)
    { }


    public void Dispose()
    {
        _connection.Dispose();
    }
}