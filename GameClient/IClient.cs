using GameModel;
using GameTransmission;

namespace GameClient;

public interface IClient
{
    public delegate void ConnectAcknowledgeEventHandler(ConnectAcknowledgeEvent e);
    public delegate void DisconnectAcknowledgeEventHandler(DisconnectAcknowledgeEvent e);
    public delegate void MoveEventHandler(MoveEvent e);
    public delegate void KillEventHandler(KillEvent e);
    public delegate void EmoteEventHandler(EmoteEvent e);
    public delegate void GameStartEventHandler(GameStartEvent e);
    public delegate void ExceptionEventHandler(ExceptionEvent e);
    public delegate void GameEndEventHandler(GameEndEvent e);
    public delegate void YourSideEventHandler(YourSideEvent e);
    
    public event ConnectAcknowledgeEventHandler? OnConnectAcknowledge;
    public event DisconnectAcknowledgeEventHandler? OnDisconnectAcknowledge;
    public event MoveEventHandler? OnMove;
    public event KillEventHandler? OnKill;
    public event EmoteEventHandler? OnEmote;
    public event GameStartEventHandler? OnGameStart;
    public event ExceptionEventHandler? OnException;
    public event GameEndEventHandler? OnGameEnd;
    public event YourSideEventHandler? OnYouSide;
}