using Common.Entity;
using static Common.Entity.EntityValues;

namespace GameModel;

public interface IGameAction
{ }

public class Action : IGameAction
{
    public Side Side { get; set; }
}

public sealed class MoveAction : Action
{
    public int From { get; set; } = InvalidInt;
    public int To { get; set; } = InvalidInt;
}

public sealed class EmoteAction : Action
{
    public Emotion Emotion { get; set; } = Emotion.Invalid;
}

public sealed class SurrenderAction : Action
{ }

public sealed class GameStopAction : IGameAction
{ }