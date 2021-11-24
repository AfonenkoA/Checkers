using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;

namespace Checkers.Data.Repository.MSSqlImplementation
{
    internal class ResourceRepository : Repository, IResourceRepository
    {
        public int CreatePicture(Credential credential, byte[] picture, string ext)
        {
            throw new System.NotImplementedException();
        }
    }
}
