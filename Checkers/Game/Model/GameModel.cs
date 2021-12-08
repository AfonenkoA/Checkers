namespace Checkers.Game.Model;

public abstract class GameModel
{
    public delegate void EmoteEventHandler(EmoteEvent e);
    public delegate void GameStartEventHandler(GameStartEvent e);
    public delegate void GameEndEventHandler(GameEndEvent e);
    public delegate void MoveEventHandler(MoveEvent e);
    public delegate void ExceptionEventHandler(ExceptionEvent e);
    public delegate void TurnEventHandler(TurnEvent e);

    public event MoveEventHandler? OnMove;
    public event EmoteEventHandler? OnEmote;
    public event ExceptionEventHandler? OnException;
    public event GameStartEventHandler? OnGameStart;
    public event GameEndEventHandler? OnGameEnd;
    public event TurnEventHandler? OnTurn;

    public readonly GameBoard Board = new();

    protected void Move(MoveEvent e)
    {
        Board.TryMove(e.From, e.To);
        OnMove?.Invoke(e);
    }

    protected void Turn(TurnEvent e) => OnTurn?.Invoke(e);

    protected void Emote(EmoteEvent e) => OnEmote?.Invoke(e);

    protected void Exception(ExceptionEvent e) => OnException?.Invoke(e);
    
    protected void End(GameEndEvent e) => OnGameEnd?.Invoke(e);

    protected void Start(GameStartEvent e) => OnGameStart?.Invoke(e);

}