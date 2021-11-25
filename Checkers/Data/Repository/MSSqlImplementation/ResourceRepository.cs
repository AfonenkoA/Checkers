using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;

namespace Checkers.Data.Repository.MSSqlImplementation
{
    public class ResourceRepository : Repository, IResourceRepository
    {
        public const string ResourceTable = "[ResourceTable]";

        public const string ResourceBytes = "[resource_bytes]";
        public const string ResourceId = "[resource_id]";
        public const string ResourceExtension = "[resource_extension]";

        public int CreatePicture(Credential credential, byte[] picture, string ext)
        {
            throw new System.NotImplementedException();
        }
    }
}
