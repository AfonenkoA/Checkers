using Common.Entity;

namespace WinFormsClient.Repository.Interface;

internal interface IResourceRepository
{
    public Task<Image> Get(Item item);
    public Task<Image> Get(PublicUserData user);
}