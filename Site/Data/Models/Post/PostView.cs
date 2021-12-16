using Site.Data.Models.User;

namespace Site.Data.Models.Post;

public sealed class PostView : PostPreview
{
    public string Content { get; }
    public DateTime Created { get; }
    public UserInfo Author { get; }
    public Chat Chat { get; }

    public PostView(Common.Entity.Post post, UserInfo author, Chat chat,string pictureUrl) : base(post, pictureUrl)
    {
        Content = post.Content;
        Created = post.Created;
        Chat = chat;
        Author = author;
    }

}