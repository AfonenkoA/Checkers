using System;

namespace Checkers.Data.Entity;


public sealed class ArticleCreationData
{
    public string Content { get; set; } = string.Empty;
    public int PictureId { get; set; } = -1;
    public string Title { get; set; } = string.Empty;
    public string Abstract { get; set; } = string.Empty;
}

public class ArticleInfo
{
    public int Id { get; set; } = -1;
    public int PictureId { get; set; } = -1;
    public string Title { get; set; } = string.Empty;
    public string Abstract { get; set; } = string.Empty;
}

public sealed class Article : ArticleInfo
{
    public int PostId { get; set; } = -1;
    public DateTime Updated { get; set; } = DateTime.MinValue;
    public static readonly Article Invalid = new();
}