using Api.Interface;
using Common.Entity;
using WinFormsClient.Repository.Interface;

namespace WinFormsClient.Repository.Implementation;

public sealed class ResourceManager : IResourceRepository
{
    private readonly IAsyncResourceService _resourceService;
    private readonly IDictionary<int, Image> _images = new Dictionary<int, Image>();

    public ResourceManager(IAsyncResourceService resource) => _resourceService = resource;

    private async Task Load(int id)
    {
        var (success, bytes) = await _resourceService.TryGetFile(id);
        if (!success) throw new ArgumentException("Resource not found");
        _images[id] = Image.FromStream(new MemoryStream(bytes));
    }

    public async Task<Image> Get(Item item)
    {
        var id = item.Resource.Id;
        if (_images.ContainsKey(id)) return _images[id];
        await Load(id);
        return _images[id];
    }

    public async Task<Image> Get(PublicUserData user)
    {
        var id = user.Picture.Resource.Id;
        if (_images.ContainsKey(id)) return _images[id];
        await Load(id);
        return _images[id];
    }
}