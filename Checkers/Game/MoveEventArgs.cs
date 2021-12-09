namespace Checkers.Game;

public sealed class MoveEventArgs : EventArgs
{
    public Position From { get; set; }
    public Position To { get; set; }
    public MoveEventArgs() : base(EventType.Move)
    { }
    public MoveEventArgs(MoveActionArgs action) : this()
    {
        From = action.From;
        To = action.To;
    }
}