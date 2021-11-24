using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Data.Entity;

public class PostInfo
{
    public int PictureId { get; set; } = -1;
    public string Title { get; set; } = string.Empty;
}

public class PostCreationData : PostInfo
{
    public string Content { get; set; } = string.Empty;
}

public sealed class Post : PostCreationData
{
    public int Id { get; set; } = -1;
    public IEnumerable<Message> Comments { get; set; } = Enumerable.Empty<Message>();
    public DateTime Updated { get; set; } = DateTime.MinValue;
}