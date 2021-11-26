namespace Checkers.Data.Repository.MSSqlImplementation;

public static class MessageRepository
{
    public const string MessageTable = "[Message]";
    public const string MessageContent = "[message_content]";
    public const string SendTime = "[send_time]";

    public const string SendMessageProc = "[SP_SendMessage]";
    public const string SelectMessageProc = "[SP_SelectMessage]";

    public const string MessageContentVar = "@message_content";
}