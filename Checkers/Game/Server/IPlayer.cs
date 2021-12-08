using Checkers.Game.Model;
using Checkers.Game.Server.Tcp;
using EmoteAction = Checkers.Game.Model.EmoteAction;
using MoveAction = Checkers.Game.Model.MoveAction;

namespace Checkers.Game.Server;

public interface IPlayer
{
    public PlayerInfo PlayerData { get; }

    public delegate void MoveActionHandler(MoveAction a);
    public delegate void EmoteActionHandler(EmoteAction a);
    public delegate void SurrenderHandler(SurrenderAction a);
    public delegate void GameRequestHandler(Player sender);
    public delegate void DisconnectActionHandler(Player sender);

    public event MoveActionHandler? OnMove;
    public event EmoteActionHandler? OnEmote;
    public event SurrenderHandler? OnSurrender;
    public event GameRequestHandler? OnGameRequest;
    public event DisconnectActionHandler? OnDisconnect;

    public void Send<T>(T obj) where T : IGameEvent;
}