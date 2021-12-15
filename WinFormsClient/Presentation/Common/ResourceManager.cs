using Api.Interface;
using Common.Entity;
using static System.Threading.Tasks.Task;

namespace WinFormsClient.Presentation.Common;

public sealed class ResourceManager
{
    private readonly IAsyncResourceService _resourceService;
    private readonly IDictionary<int, Image> _images = new Dictionary<int, Image>();

    public ResourceManager(IAsyncResourceService resource) => _resourceService = resource;

    public Task PreLoad(IEnumerable<Item> resources) => WhenAll(resources.Select(Load));

    private async Task Load(Item item)
    {
        var (success, bytes) = await _resourceService.TryGetFile(item.Resource.Id);
        if (!success) throw new ArgumentException("Resource not found");
        _images.Add(item.Resource.Id, Image.FromStream(new MemoryStream(bytes)));
    }

    public Image Get(Item item) => _images[item.Resource.Id];

    public async Task<Image> GetAsync(Item item)
    {
        if (_images.ContainsKey(item.Resource.Id)) return _images[item.Resource.Id];
        await Load(item);
        return _images[item.Resource.Id];
    }

}