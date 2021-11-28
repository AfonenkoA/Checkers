using System;
using System.Text.Json.Serialization;
using static Checkers.Data.Entity.EntityValues;

namespace Checkers.Data.Entity;

public sealed class ArticleCreationData
{
    public string Content { get; set; } = InvalidString;
    public int PictureId { get; set; } = InvalidId;
    public string Title { get; set; } = InvalidString;
    public string Abstract { get; set; } = InvalidString;

    [JsonIgnore]
    public bool IsValid => !(Content == InvalidString ||
                             PictureId == InvalidId ||
                             Title == InvalidString ||
                             Abstract == InvalidString);
}

public class ArticleInfo
{
    public int Id { get; set; } = InvalidId;
    public int PictureId { get; set; } = InvalidId;
    public string Title { get; set; } = InvalidString;
    public string Abstract { get; set; } = InvalidString;

    [JsonIgnore]
    public virtual bool IsValid => !(Id == InvalidId ||
                             PictureId == InvalidId ||
                             Title == InvalidString ||
                             Abstract == InvalidString);

    public static readonly ArticleInfo Invalid = new();
}

public sealed class Article : ArticleInfo
{
    public Article(ArticleInfo data)
    {
        Id = data.Id;
        PictureId = data.PictureId;
        Title = data.Title;
        Abstract = data.Abstract;
    }

    public string Content { get; set; } = InvalidString;
    public int PostId { get; set; } = InvalidId;
    public DateTime Created { get; set; } = InvalidDate;
    public new static readonly Article Invalid = new(ArticleInfo.Invalid);

    [JsonIgnore]
    public override bool IsValid => base.IsValid &&
                                    !(Content == InvalidString ||
                                      PostId == InvalidId ||
                                      Created == InvalidDate);
}