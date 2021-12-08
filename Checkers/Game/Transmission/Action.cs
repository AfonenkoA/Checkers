using System.Text.Json.Serialization;
using Checkers.Data.Entity;

namespace Checkers.Game.Transmission;

public class Action : Message
{
    protected Action(string type) : base(type) { }
    [JsonConstructor]
    public Action() { }
}

public sealed class MoveAction : Action
{
    public Model.MoveAction Move { get; set; }

    public MoveAction(Model.MoveAction a) : this() =>  Move = a;
    
    public MoveAction() : base(nameof(MoveAction)) { }
}

public sealed class EmoteAction : Action
{
    public Model.EmoteAction Emote { get; set; }
    public EmoteAction(Model.EmoteAction a) : this() => Emote = a;
    public EmoteAction() : base(nameof(EmoteAction)) { }
}

public sealed class SurrenderAction : Action
{
    public Model.SurrenderAction Surrender { get; set; }
    public SurrenderAction(Model.SurrenderAction a) : this() => Surrender = a;
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
