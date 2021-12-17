﻿using Common.Entity;
using Site.Data.Models.Post;

namespace Site.Repository.Interface;

public interface IForumRepository
{
    public Task<(bool, VisualPost)> GetPost(ICredential c, int postId);
    public Task<(bool, IEnumerable<Preview>)> GetPosts();

    public Task<bool> Create(CreationData creation);
}