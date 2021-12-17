using Common.Entity;
using WinFormsClient.Model;

namespace WinFormsClient.Repository.Interface;

public interface IUserRepository
{
    public Task<(bool, Self)> GetSelf(Credential c);
    public Task<(bool, Collection)> GetCollection(Credential c);
}