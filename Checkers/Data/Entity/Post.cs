using System;
using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;

public class PostCreationData
{
    public string Content { get; init; } = InvalidString;
    public int PictureId { get; init; } = InvalidId;
    public string Title { get; init; } = InvalidString;

    [JsonIgnore]
    public virtual bool IsValid => !(Content == InvalidString ||
                                     PictureId == InvalidId ||
                                     Title == InvalidString);
}

public class PostInfo : PostCreationData
{
    public static readonly PostInfo Invalid = new();

    public int Id { get; init; } = InvalidId;
    [JsonIgnore] public override bool IsValid => base.IsValid && Id != InvalidId;
}

public sealed class Post : PostInfo
{
    public new static readonly Post Invalid = new(PostInfo.Invalid);
    public Post(PostInfo data)
    {
        Id = data.Id;
        Title = data.Title;
        Content = data.Content;
        PictureId = data.PictureId;
    }
    public int AuthorId { get; init; } = InvalidId;
    public int ChatId { get; init; } = InvalidId;
    public DateTime Created { get; init; } = InvalidDate;

    [JsonIgnore]
    public override bool IsValid => base.IsValid && !(AuthorId == InvalidId ||
                                                      ChatId == InvalidId ||
                                                      Created == InvalidDate);
}