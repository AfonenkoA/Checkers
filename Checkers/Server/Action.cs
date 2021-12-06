using Checkers.Data.Entity;

namespace Checkers.Server;

public class Action : ComMessage
{
    protected Action(string type) : base(type) { }
    public Action() { }
}

public sealed class ConnectionAction : Action
{
    public ConnectionAction() : base(nameof(ConnectionAction)) { }
    public Credential Credential { get; set; } = new();
}

public sealed class DisconnectAction : Action
{
    public DisconnectAction() : base(nameof(DisconnectAction)) { }
}

public sealed class MoveAction : Action
{
    public MoveAction() : base(nameof(MoveAction)) { }
}

public sealed class EmoteAction : Action
{
    public EmoteAction() : base(nameof(EmoteAction)) { }
}

public sealed class SurrenderAction : Action
{
    public SurrenderAction() : base(nameof(SurrenderAction)) { }
}

public sealed class RequestForGameAction : Action
{
    public RequestForGameAction() : base(nameof(RequestForGameAction)) { }
}