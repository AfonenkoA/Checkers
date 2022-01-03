using Api.Interface;
using Site.Data.Models;
using Site.Service.Interface;

namespace Site.Service.Implementation;

public sealed class ItemService : IItemService
{
    private readonly IAsyncItemApi _item;
    private readonly IResourceService _resource;

    public ItemService(IAsyncItemApi item, IResourceService repository)
    {
        _item = item;
        _resource = repository;
    }

    public async Task<IEnumerable<PictureView>> GetPictures()
    {
        var (_, data) = await _item.TryGetPictures();
        return data.Select(p => new PictureView(_resource.GetResource(p.Resource.Id),p.Id));
    }

    public async Task<PictureView> GetPicture(int id)
    {
        var (_, pic) = await _item.TryGetPicture(id);
        return new PictureView(_resource.GetResource(pic.Resource.Id), id);
    }
}