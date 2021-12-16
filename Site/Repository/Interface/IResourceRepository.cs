using Site.Data.Models;

namespace Site.Repository.Interface;

public interface IResourceRepository
{
    public ResourceView GetResource(int id);
}