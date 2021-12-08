using System;
using Checkers.Data.Entity;
using Checkers.Game.Model;
using Checkers.Game.Server.Repository;
using Checkers.Game.Transmission;
using static Checkers.CommunicationProtocol;
using static Checkers.Game.Server.IPlayer;
using Action = Checkers.Game.Transmission.Action;
using EmoteAction = Checkers.Game.Transmission.EmoteAction;
using MoveAction = Checkers.Game.Transmission.MoveAction;
using SurrenderAction = Checkers.Game.Model.SurrenderAction;

namespace Checkers.Game.Server.Tcp;

public sealed class Player : IPlayer, IDisposable
{
    private readonly Connection _connection;
    private readonly IPlayerRepository _repository;
    private readonly Credential _credential;


    internal Player(IPlayerRepository repository, Connection connection, Credential credential)
    {
        _connection = connection;
        _repository = repository;
        _credential = credential;
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
                    OnGameRequest?.Invoke(this);
                    break;
                case nameof(DisconnectRequestAction):
                    OnDisconnect?.Invoke(this);
                    break;
                case nameof(MoveAction):
                    {
                        var move = Deserialize<MoveAction>(json);
                        if (move == null) break;
                        OnMove?.Invoke(move.Move);
                        break;
                    }
                case nameof(SurrenderAction):
                    {
                        var s = Deserialize<SurrenderAction>(json);
                        if (s == null) break;
                        OnSurrender?.Invoke(s);
                        break;
                    }
                case nameof(EmoteAction):
                    {
                        var emote = Deserialize<EmoteAction>(json);
                        if (emote == null) break;
                        OnEmote?.Invoke(emote.Emote);
                        break;
                    }
            }
        }
    }

    public void Dispose() => _connection.Dispose();

    public PlayerInfo PlayerData => _repository.GetInfo(_credential);
    public event MoveActionHandler? OnMove;
    public event EmoteActionHandler? OnEmote;
    public event SurrenderHandler? OnSurrender;
    public event GameRequestHandler? OnGameRequest;
    public event DisconnectActionHandler? OnDisconnect;

    public void Send<T>(T obj) where T : IGameEvent => _connection.Transmit(obj).Wait();
}
