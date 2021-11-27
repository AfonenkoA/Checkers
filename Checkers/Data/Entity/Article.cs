using System;
using static System.String;

namespace Checkers.Data.Entity;


public sealed class ArticleCreationData
{
    public string Content { get; set; } = Empty;
    public int PictureId { get; set; } = -1;
    public string Title { get; set; } = Empty;
    public string Abstract { get; set; } = Empty;
}

public class ArticleInfo
{
    public int Id { get; set; } = -1;
    public int PictureId { get; set; } = -1;
    public string Title { get; set; } = Empty;
    public string Abstract { get; set; } = Empty;

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
    public string Content { get; set; } = Empty;
    public int PostId { get; set; } = -1;
    public DateTime Updated { get; set; } = DateTime.MinValue;
    public new static readonly Article Invalid = new(ArticleInfo.Invalid);
}