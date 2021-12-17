namespace Site.Data.Models.Post;

public sealed class CreationData
{
    public IFormFile? Picture { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
}