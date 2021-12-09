using Common.Entity;
using static Common.Entity.Credential;

namespace GameTransmission;

public sealed class GameRequestAction
{ }


public sealed class ConnectRequestAction
{
    public Credential Credential { get; set; } = Invalid;
}

public sealed class DisconnectRequestAction
{ }
