﻿using Common.Entity;
using WinFormsClient.Model;

namespace WinFormsClient.Repository.Interface;

public interface IUserRepository
{
    public Task<(bool, Self)> GetSelf(ICredential c);
    public Task<(bool, Collection)> GetCollection(ICredential c);
    public Task<bool> SelectAnimation(ICredential credential, int id);
    public Task<bool> SelectCheckers(ICredential credential, int id);

    public Task<(bool, IEnumerable<Model.User>)> GetFriends(ICredential c);
}