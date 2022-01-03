using Site.Data.Models;

namespace Site.Service.Interface;

public interface IItemService
{
    public Task<IEnumerable<PictureView>> GetPictures();
    public Task<PictureView> GetPicture(int id);
}