using static System.Array;

namespace GameModel;

public sealed class Board
{
    public enum State
    {
        Empty,
        WhiteChecker,
        BlackChecker,
        WhiteKing,
        BlackKing
    }

    internal Board() { }
    public State[,] DetailedState => new State[8, 8];
    public State[] ShortState => new State[32];
    internal bool TryMove(int from, int to) => true;
    public int[] GetAvailableMove(int from) => Empty<int>();
}