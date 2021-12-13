using Common.Entity;

namespace WebService.Repository.Interface;

internal interface IResourceRepository
{
    int CreateFile(Credential credential, byte[] picture, string ext);
    (byte[], string) GetFile(int id);
}