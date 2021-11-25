using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface;

public interface IResourceRepository
{
    int CreateFile(Credential credential, byte[] picture, string ext);
    (byte[], string) GetFile(int id);
}