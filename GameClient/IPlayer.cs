using GameModel;

namespace GameClient;

public interface IPlayer
{
    public delegate void MoveEventHandler(MoveEvent e);
    public delegate void TurnEventHandler(TurnEvent e);
    public delegate void EmoteEventHandler(EmoteEvent e);
    public delegate void GameStartEventHandler(GameStartEvent e);
    public delegate void ExceptionEventHandler(ExceptionEvent e);
    public delegate void GameEndEventHandler(GameEndEvent e);
    public delegate void YourSideEventHandler(YourSideEvent e);

    public event MoveEventHandler? OnMove;
    public event EmoteEventHandler? OnEmote;
    public event GameStartEventHandler? OnGameStart;
    public event ExceptionEventHandler? OnException;
    public event GameEndEventHandler? OnGameEnd;
    public event YourSideEventHandler? OnYouSide;
    public event TurnEventHandler? OnTurn;

    public Task Send<T>(T obj) where T : IGameAction;
    public void Listen();

}