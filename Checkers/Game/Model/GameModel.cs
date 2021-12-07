using System.Linq;
using System.Threading.Tasks;
using Checkers.Data.Entity;
using static Checkers.Data.Entity.Side;

namespace Checkers.Game.Model;

public abstract class GameModel
{
    private const string MoveExceptionMessage = "Move action out of turn";

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

    private static readonly TurnEvent WhiteTurnEvent = new() { Side = White };
    private static readonly TurnEvent BlackTurnEvent = new() { Side = Black };
    
    private Side _turn = White;
    public readonly GameBoard Board = new();

    protected void Move(Side side, int from, int to)
    {
        if (_turn != side)
            Exception(MoveExceptionMessage);
        OnMove?.Invoke(new MoveEvent
        {
            From = from,
            To = to,
            Side = side
        });
        if (!Board.GetAvailableMove(to).Any())
            Invert(ref side);
        SetTurn(side);
    }

    private static void Invert(ref Side s) => s = s == White ? Black : White;

    private void SetTurn(Side side)
    {
        _turn = side;
        OnTurn?.Invoke(side == White ? WhiteTurnEvent : BlackTurnEvent);
    }

    protected void Emote(EmoteEvent e) => OnEmote?.Invoke(e);

    protected void Exception(string m) =>
        OnException?.Invoke(new ExceptionEvent { Message = m });

    protected void End(GameEndEvent e) => OnGameEnd?.Invoke(e);

    protected void Start(GameStartEvent e) => OnGameStart?.Invoke(e);


    public abstract Task Run();
}