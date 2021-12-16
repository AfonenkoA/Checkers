using Site.Data.Models;

namespace Site.Repository.Interface;

public interface IItemRepository
{
    public Task<IEnumerable<PictureView>> GetPictures();
    public Task<PictureView> GetPicture(int id);
}