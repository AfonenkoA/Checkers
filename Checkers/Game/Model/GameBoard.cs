namespace Checkers.Game.Model;

public sealed class GameBoard
{
    public enum State
    {
        Empty,
        WhiteChecker,
        BlackChecker,
        WhiteKing,
        BlackKing
    }

    internal GameBoard() { }
    public State[,] DetailedState => new State[8, 8];
    public State[] ShortState => new State[32];
    private void TryMove(int from, int to) { }
    public int[] GetAvailableMove(int from) => new int[] { };
}