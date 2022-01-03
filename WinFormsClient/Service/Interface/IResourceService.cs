using Common.Entity;

namespace WinFormsClient.Service.Interface;

internal interface IResourceService
{
    public Task<Image> Get(Item item);
    public Task<Image> Get(PublicUserData user);
}