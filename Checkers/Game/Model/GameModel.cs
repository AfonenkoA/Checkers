using System;
using Checkers.Game.Server.Transmission;

namespace Checkers.Game.Model;

public interface IGameController
{
    void Move(MoveAction a);
    void Emote(EmoteAction a);
    void Surrender();
}

public enum Color
{
    White,
    Black
}

public abstract class GameModel
{
    public sealed class GameController : IGameController
    {
        private readonly Color _color;
        private readonly GameModel _model;

        public GameController(GameModel model, Color color)
        {
            _color = color;
            _model = model;
        }

        public void Move(MoveAction a)
        {
            _model.Move(_color, a);
        }

        public void Emote(EmoteAction a)
        {
            _model.Emote(_color, a);
        }

        public void Surrender()
        {
            _model.Surrender(_color);
        }
    }

    public delegate void EmoteEventHandler(EmoteEvent e);
    public delegate void GameStartEventHandler(GameStartEvent e);
    public delegate void GameEndEventHandler(GameEndEvent e);
    public delegate void MoveEventHandler(MoveEvent e);

    public event MoveEventHandler? OnMove;
    public event EmoteEventHandler? OnEmote;
    public event EventHandler? OnExecption;
    public event GameStartEventHandler? OnGameStart;
    public event GameEndEventHandler? OnGameEnd;

    protected Color Turn;
    public readonly GameBoard Board = new GameBoard();

    internal GameModel() { }

    private static void ValidateMove() { }

    protected void Move(Color color, MoveAction args)
    {
        ValidateMove();
        OnMove?.Invoke(new MoveEvent());
        if(true)
            OnGameEnd?.Invoke(new GameEndEvent());
    }

    protected void Emote(Color color, EmoteAction args)
    {
        OnEmote?.Invoke(new EmoteEvent());
    }

    protected void Surrender(Color color)
    {}

    protected void Start()
    {
        OnGameStart?.Invoke(new GameStartEvent());
    }

    public abstract void Run();

    private void TryMove(int from, int to) { }
    public int[] GetAvailableMove(int from) => new int[] { };
}