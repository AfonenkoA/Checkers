namespace Checkers.Data.Repository.MSSqlImplementation;

public class MessageRepository : Repository
{
    public const string MessageTable = "[Message]";
    public const string MessageContent = "[message_content]";
    public const string SendTime = "[send_time]";
}