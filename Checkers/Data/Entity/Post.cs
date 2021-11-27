using System;

namespace Checkers.Data.Entity;

public class PostCreationData
{
    public string Content { get; set; } = string.Empty;
    public int PictureId { get; set; } = -1;
    public string Title { get; set; } = string.Empty;
}

public class PostInfo : PostCreationData
{
    public int Id { get; set; } = -1;
    public static readonly PostInfo Invalid = new();
}

public sealed class Post : PostInfo
{
    public Post(PostInfo data)
    {
        Id = data.Id;
        Title = data.Title;
        Content = data.Content;
        PictureId = data.PictureId;
    }
    public int AuthorId { get; set; } = -1;
    public int ChatId { get; set; } = -1;
    public DateTime Created { get; set; } = DateTime.MinValue;
    public new static readonly Post Invalid = new(PostInfo.Invalid);
}