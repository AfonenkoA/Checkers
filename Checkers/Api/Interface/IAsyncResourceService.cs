using System.Threading.Tasks;
using Checkers.Data;
using Checkers.Data.Entity;

namespace Checkers.Api.Interface;

public interface IAsyncResourceService
{
    Task<(bool,int)> TryUploadPicture(Credential credential, byte[] picture, string ext);
    string GetPictureUrl(int id);
}