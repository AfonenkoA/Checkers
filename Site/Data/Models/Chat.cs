namespace Site.Data.Models;

public class Chat
{
    public Chat(int id, IEnumerable<MessageView> messages)
    {
        Id = id;
        Messages = messages;
    }

    public int Id { get;}
    public IEnumerable<MessageView> Messages { get; }
}