using System.Text.Json.Serialization;

namespace GameTransmission;

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
    public GameModel.MoveEvent Move { get; set; }
    public MoveEvent() : base(nameof(MoveEvent)) { }

    public MoveEvent(GameModel.MoveEvent move) : this() => Move = move;
}

public sealed class EmoteEvent : Event
{
    public GameModel.EmoteEvent Emote { get; set; }
    public EmoteEvent() : base(nameof(EmoteEvent)) { }
    public EmoteEvent(GameModel.EmoteEvent emote) : this() => Emote = emote;
}

public sealed class TurnEvent : Event
{
    public GameModel.TurnEvent Turn { get; set; }
    public TurnEvent() : base(nameof(TurnEvent)) { }

    public TurnEvent(GameModel.TurnEvent turn) : this() => Turn = turn;
}

public sealed class GameStartEvent : Event
{
    public GameModel.GameStartEvent GameStart { get; set; }
    public GameStartEvent() : base(nameof(GameStartEvent)) { }

    public GameStartEvent(GameModel.GameStartEvent gameStart) : this() => GameStart = gameStart;
}

public sealed class GameEndEvent : Event
{
    public GameModel.GameEndEvent GameEnd { get; set; }
    public GameEndEvent() : base(nameof(GameEndEvent)) { }
    public GameEndEvent(GameModel.GameEndEvent gameEnd) : this() => GameEnd = gameEnd;
}