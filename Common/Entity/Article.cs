﻿using System.Text.Json.Serialization;
using static Common.Entity.EntityValues;

namespace Common.Entity;

public sealed class ArticleCreationData
{
    public string Content { get; set; } = InvalidString;
    public int PictureId { get; set; } = InvalidInt;
    public string Title { get; set; } = InvalidString;
    public string Abstract { get; set; } = InvalidString;

    [JsonIgnore]
    public bool IsValid => !(Content == InvalidString ||
                             PictureId == InvalidInt ||
                             Title == InvalidString ||
                             Abstract == InvalidString);
}

public class ArticleInfo
{
    public int Id { get; init; } = InvalidInt;
    public int PictureId { get; init; } = InvalidInt;
    public string Title { get; init; } = InvalidString;
    public string Abstract { get; init; } = InvalidString;

    [JsonIgnore]
    public virtual bool IsValid => !(Id == InvalidInt ||
                             PictureId == InvalidInt ||
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

    [JsonConstructor]
    public Article() { }
    public string Content { get; init; } = InvalidString;
    public int PostId { get; init; } = InvalidInt;
    public DateTime Created { get; init; } = InvalidDate;
    public new static readonly Article Invalid = new(ArticleInfo.Invalid);

    [JsonIgnore]
    public override bool IsValid => base.IsValid &&
                                    !(Content == InvalidString ||
                                      PostId == InvalidInt ||
                                      Created == InvalidDate);
}