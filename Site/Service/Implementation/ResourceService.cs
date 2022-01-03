using Api.Interface;
using Common.Entity;
using Site.Data.Models;
using Site.Service.Interface;

namespace Site.Service.Implementation;

public sealed class ResourceService : IResourceService
{
    private readonly IAsyncResourceService _resource;

    public ResourceService(IAsyncResourceService resource)
    {
        _resource = resource;
    }

    public ResourceView GetResource(int id)
    {
        return new ResourceView(_resource.GetFileUrl(id));
    }

    public async Task<(bool, int)> Create(ICredential credential, IFormFile file)
    {
        await using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var extension = Path.GetExtension(file.FileName);
        return await _resource.TryUploadFile(credential, ms.GetBuffer(), extension);

    }
}