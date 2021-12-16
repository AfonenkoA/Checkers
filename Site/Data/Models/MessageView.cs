using Common.Entity;
using Site.Data.Models.User;

namespace Site.Data.Models;

public class MessageView
{
    public int Id { get; }
    public UserInfo Sender { get; }
    public string Content { get; }
    public DateTime SendTime { get; }
    public MessageView(UserInfo sender, Message message)
    {
        Id = message.Id;
        Content = message.Content;
        SendTime = message.SendTime;
        Sender = sender;
    }
}