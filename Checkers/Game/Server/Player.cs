using Checkers.Data.Entity;
using Checkers.Game.Transmission;
using System;
using Checkers.Game.Model;
using static Checkers.CommunicationProtocol;
using static Checkers.Game.Server.IPlayer;
using Action = Checkers.Game.Transmission.Action;
namespace Checkers.Game.Server;

public sealed class Player : IPlayer, IDisposable
{
    private readonly Connection _connection;
    private Credential Credential { get; }


    internal Player(Connection connection, Credential credential)
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

    public void Dispose() => _connection.Dispose();

    public PlayerInfo PlayerData { get; }
    public event MoveActionHandler? OnMove;
    public event EmoteActionHandler? OnEmote;
    public event SurrenderHandler? OnSurrender;
    public event GameRequestHandler? OnGameRequest;
    public event DisconnectActionHandler? OnDisconnect;

    public void Send<T>(T obj) where T : IGameEvent
    {
        throw new NotImplementedException();
    }
}
