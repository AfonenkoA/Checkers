using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface;

internal interface IResourceRepository
{
    int CreatePicture(Credential credential, byte[] picture, string ext);
}