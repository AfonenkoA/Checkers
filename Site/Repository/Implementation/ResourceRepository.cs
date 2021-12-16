using Api.Interface;
using Site.Data.Models;
using Site.Repository.Interface;

namespace Site.Repository.Implementation;

public sealed class ResourceRepository : IResourceRepository
{
    private readonly IAsyncResourceService _resource;

    public ResourceRepository(IAsyncResourceService resource)
    {
        _resource = resource;
    }

    public ResourceView GetResource(int id)
    {
        return new ResourceView(_resource.GetFileUrl(id));
    }
}