using Common.Entity;
using static Common.Entity.Credential;

namespace GameTransmission;

internal interface ITransmissionAction { }

public sealed class GameRequestAction : ITransmissionAction
{ }

public sealed class ConnectRequestAction : ITransmissionAction
{
    public Credential Credential { get; set; } = Invalid;
}

public sealed class DisconnectRequestAction : ITransmissionAction
{ }
