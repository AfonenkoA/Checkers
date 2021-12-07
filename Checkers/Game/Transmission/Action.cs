using Checkers.Data.Entity;

namespace Checkers.Game.Transmission;

public class Action : Message
{
    protected Action(string type) : base(type) { }
    public Action() { }
}

public sealed class MoveAction : Action
{
    public int From { get; set; }
    public int To { get; set; }
    public MoveAction() : base(nameof(MoveAction)) { }
}

public sealed class EmoteAction : Action
{
    public int EmotionId { get; set; }
    public EmoteAction() : base(nameof(EmoteAction)) { }
}

public sealed class SurrenderAction : Action
{
    public SurrenderAction() : base(nameof(SurrenderAction)) { }
}

public sealed class GameRequestAction : Action
{
    public GameRequestAction() : base(nameof(GameRequestAction)) { }
}


public sealed class ConnectRequestAction : Action
{
    public ConnectRequestAction() : base(nameof(ConnectRequestAction)) { }
    public Credential Credential { get; set; } = new();
}

public sealed class DisconnectRequestAction : Action
{
    public DisconnectRequestAction() : base(nameof(DisconnectRequestAction)) { }
}
