using Site.Data.Models.UserIdentity;

namespace Site.Data.Models;

public sealed class PictureUpdateData : Identity
{
    public int Id { get; set; }
    public IFormFile? Picture { get; set; }
}