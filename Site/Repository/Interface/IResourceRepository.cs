using Common.Entity;
using Site.Data.Models;

namespace Site.Repository.Interface;

public interface IResourceRepository
{
    public ResourceView GetResource(int id);
    public Task<(bool, int)> Create(ICredential credential, IFormFile file);
}