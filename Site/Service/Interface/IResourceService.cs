using Common.Entity;
using Site.Data.Models;

namespace Site.Service.Interface;

public interface IResourceService
{
    public ResourceView GetResource(int id);
    public Task<(bool, int)> Create(ICredential credential, IFormFile file);
}