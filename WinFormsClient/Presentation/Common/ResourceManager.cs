using Api.Interface;

namespace WinFormsClient.Presentation.Common;

internal sealed class ResourceManager
{
    private readonly IAsyncResourceService _resourceService;
    private readonly IDictionary<int, Image> _images = new Dictionary<int, Image>();

    public ResourceManager(IAsyncResourceService resource) => _resourceService = resource;

    public async Task<Image> Get(int resourceId)
    {
        if (_images.ContainsKey(resourceId)) return _images[resourceId];
        var (success, bytes) = await _resourceService.TryGetFile(resourceId);
        if (!success) throw new ArgumentException();
        _images.Add(resourceId, Image.FromStream(new MemoryStream(bytes)));
        return _images[resourceId];
    }

}