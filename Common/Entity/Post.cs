using System.Text.Json.Serialization;
using static Common.Entity.EntityValues;

namespace Common.Entity;

public class PostCreationData
{
    public string Content { get; init; } = InvalidString;
    public int PictureId { get; init; } = InvalidInt;
    public string Title { get; init; } = InvalidString;

    [JsonIgnore]
    public virtual bool IsValid => !(Content == InvalidString ||
                                     PictureId == InvalidInt ||
                                     Title == InvalidString);
}

public class PostInfo : PostCreationData
{
    public static readonly PostInfo Invalid = new();

    public int Id { get; init; } = InvalidInt;
    [JsonIgnore] public override bool IsValid => base.IsValid && Id != InvalidInt;
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
    [JsonConstructor]
    public Post(){}
    public int AuthorId { get; init; } = InvalidInt;
    public int ChatId { get; init; } = InvalidInt;
    public DateTime Created { get; init; } = InvalidDate;

    [JsonIgnore]
    public override bool IsValid => base.IsValid && !(AuthorId == InvalidInt ||
                                                      ChatId == InvalidInt ||
                                                      Created == InvalidDate);
}