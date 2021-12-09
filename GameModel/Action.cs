using static Common.Entity.EntityValues;

namespace GameModel;

public class Action
{
    public Side Side { get; set; }
}

public sealed class MoveAction : Action
{
    public int Form { get; set; } = InvalidInt;
    public int To { get; set; } = InvalidInt;
}

public sealed class EmoteAction : Action
{
    public int Id { get; set; } = InvalidInt;
}

public sealed class SurrenderAction : Action
{ }