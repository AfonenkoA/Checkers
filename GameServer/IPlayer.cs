using GameModel;
using EmoteAction = GameModel.EmoteAction;
using MoveAction = GameModel.MoveAction;

namespace GameServer;

public interface IPlayer
{
    public PlayerInfo PlayerData { get; }

    public delegate void MoveActionHandler(MoveAction a);
    public delegate void EmoteActionHandler(EmoteAction a);
    public delegate void SurrenderHandler(SurrenderAction a);
    public delegate void GameRequestHandler(IPlayer sender);
    public delegate void DisconnectActionHandler(IPlayer sender);

    public event MoveActionHandler? OnMove;
    public event EmoteActionHandler? OnEmote;
    public event SurrenderHandler? OnSurrender;
    public event GameRequestHandler? OnGameRequest;
    public event DisconnectActionHandler? OnDisconnect;

    public void Send<T>(T obj);
    void Listen();
}