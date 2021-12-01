using System.Threading.Tasks;
using Checkers.Data.Entity;

namespace Checkers.Api.Interface;

public interface IAsyncResourceService
{
    Task<(bool,int)> TryUploadFile(Credential credential, byte[] picture, string ext);
    Task<(bool, byte[])> TryGetFile(int id);
    string GetFileUrl(int id);
}