using Common.Entity;

namespace Api.Interface;

public interface IAsyncResourceService
{
    Task<(bool, int)> TryUploadFile(ICredential credential, byte[] picture, string ext);
    Task<(bool, byte[])> TryGetFile(int id);
    string GetFileUrl(int id);
}