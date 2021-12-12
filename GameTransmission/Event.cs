namespace GameTransmission;

public interface ITransmissionEvent 
{}

public sealed class ConnectAcknowledgeEvent : ITransmissionEvent
{ }

public sealed class DisconnectAcknowledgeEvent : ITransmissionEvent
{ }

