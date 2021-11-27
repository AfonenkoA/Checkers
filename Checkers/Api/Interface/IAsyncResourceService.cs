using System.Threading.Tasks;
using Checkers.Data.Entity;

namespace Checkers.Api.Interface;

public interface IAsyncResourceService
{
    Task<(bool,int)> TryUploadFile(Credential credential, byte[] picture, string ext);
    string GetFileUrl(int id);
}