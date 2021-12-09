using System.Text.Json.Serialization;
using Common.Entity;

namespace GameTransmission;

public class Action : Message
{
    protected Action(string type) : base(type) { }
    [JsonConstructor]
    public Action() { }
}

public sealed class MoveAction : Action
{
    public GameModel.MoveAction Move { get; set; }

    public MoveAction(GameModel.MoveAction a) : this() =>  Move = a;
    
    public MoveAction() : base(nameof(MoveAction)) { }
}

public sealed class EmoteAction : Action
{
    public GameModel.EmoteAction Emote { get; set; }
    public EmoteAction(GameModel.EmoteAction a) : this() => Emote = a;
    public EmoteAction() : base(nameof(EmoteAction)) { }
}

public sealed class SurrenderAction : Action
{
    public GameModel.SurrenderAction Surrender { get; set; }
    public SurrenderAction(GameModel.SurrenderAction a) : this() => Surrender = a;
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
