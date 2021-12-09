using GameModel;
using GameServer.Tcp;
using EmoteAction = GameModel.EmoteAction;
using MoveAction = GameModel.MoveAction;

namespace GameServer;

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