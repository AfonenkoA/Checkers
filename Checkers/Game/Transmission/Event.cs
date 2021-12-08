using System.Text.Json.Serialization;

namespace Checkers.Game.Transmission;

public class Event : Message
{
    protected Event(string type) : base(type) { }
    [JsonConstructor]
    public Event() { }
}

public sealed class ConnectAcknowledgeEvent : Event
{
    public ConnectAcknowledgeEvent() : base(nameof(ConnectAcknowledgeEvent)) { }
}

public sealed class DisconnectAcknowledgeEvent : Event
{
    public DisconnectAcknowledgeEvent() : base(nameof(DisconnectAcknowledgeEvent)) { }
}

public sealed class MoveEvent : Event
{
    public Model.MoveEvent Move { get; set; }
    public MoveEvent() : base(nameof(MoveEvent)) { }

    public MoveEvent(Model.MoveEvent move) : this() => Move = move;
}

public sealed class EmoteEvent : Event
{
    public Model.EmoteEvent Emote { get; set; }
    public EmoteEvent() : base(nameof(EmoteEvent)) { }
    public EmoteEvent(Model.EmoteEvent emote) : this() => Emote = emote;
}

public sealed class TurnEvent : Event
{
    public Model.TurnEvent Turn { get; set; }
    public TurnEvent() : base(nameof(TurnEvent)) { }

    public TurnEvent(Model.TurnEvent turn) : this() => Turn = turn;
}

public sealed class GameStartEvent : Event
{
    public Model.GameStartEvent GameStart { get; set; }
    public GameStartEvent() : base(nameof(GameStartEvent)) { }

    public GameStartEvent(Model.GameStartEvent gameStart) : this() => GameStart = gameStart;
}

public sealed class GameEndEvent : Event
{
    public Model.GameEndEvent GameEnd { get; set; }
    public GameEndEvent() : base(nameof(GameEndEvent)) { }
    public GameEndEvent(Model.GameEndEvent gameEnd) : this() => GameEnd = gameEnd;
}