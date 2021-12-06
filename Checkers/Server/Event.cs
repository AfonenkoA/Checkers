namespace Checkers.Server;

public class Event : ComMessage
{
    protected Event(string type) : base(type) { }
    public Event() { }
}

public sealed class ConnectionAcceptEvent : Event
{
    public ConnectionAcceptEvent() : base(nameof(ConnectionAcceptEvent)) { }
    public bool IsAccepted { get; set; } = false;
}

public sealed class MoveEvent : Action
{
    public MoveEvent() : base(nameof(MoveEvent)) { }
}

public sealed class EmoteEvent : Action
{
    public EmoteEvent() : base(nameof(EmoteEvent)) { }
}

public sealed class GameEndEvent : Action
{
    public GameEndEvent() : base(nameof(GameEndEvent)) { }
}

public sealed class GameStartEvent : Action
{
    public GameStartEvent() : base(nameof(GameStartEvent)) { }
}