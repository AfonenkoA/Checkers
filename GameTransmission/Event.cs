namespace GameTransmission;

internal interface ITransmissionEvent
{ }

public sealed class GameAcknowledgeEvent : ITransmissionEvent
{ }

public sealed class ConnectAcknowledgeEvent : ITransmissionEvent
{ }

public sealed class DisconnectAcknowledgeEvent : ITransmissionEvent
{ }

