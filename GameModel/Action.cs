namespace GameModel;

public class Action
{
    public Side Side { get; set; }
}

public sealed class MoveAction : Action
{
    public int Form { get; set; }
    public int To { get; set; }
}

public sealed class EmoteAction : Action
{
    public int Id { get; set; }
}

public sealed class SurrenderAction : Action
{ }