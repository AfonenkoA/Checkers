using Site.Data.Models.User;

namespace Site.Data.Models.Post;

public sealed class VisualPost : Preview
{
    public string Content { get; }
    public DateTime Created { get; }
    public UserInfo Author { get; }
    public Chat Chat { get; }

    public VisualPost(Common.Entity.Post post, UserInfo author, Chat chat, ResourceView pic) : base(post, pic)
    {
        Content = post.Content;
        Created = post.Created;
        Chat = chat;
        Author = author;
    }

}