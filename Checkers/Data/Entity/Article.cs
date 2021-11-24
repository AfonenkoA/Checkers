using System;

namespace Checkers.Data.Entity;

public class ArticleInfo
{
    public int PictureId { get; set; } = -1;
    public string Title { get; set; } = string.Empty;
    public string Abstract { get; set; } = string.Empty;
}

public class ArticleCreationData : ArticleInfo
{
    public string Content { get; set; } = string.Empty;
}

public sealed class Article : ArticleCreationData
{
    public int Id { get; set; } = -1;
    public int PostId { get; set; } = -1;
    public DateTime Updated { get; set; } = DateTime.MinValue;
    public static readonly Article Invalid = new();
}