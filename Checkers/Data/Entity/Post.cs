using System;
using System.Collections.Generic;
using System.Linq;

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
}

public sealed class Post : PostInfo
{
    public IEnumerable<Message> Comments { get; set; } = Enumerable.Empty<Message>();
    public DateTime Created { get; set; } = DateTime.MinValue;
}