using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest;

[TestClass]
public class ForumTest
{
    private static readonly IAsyncForumApi ForumApi = new ForumWebApi();
    private static readonly List<PostInfo> Posts = new();

    [TestMethod]
    public async Task Test01GetAllPosts()
    {
        var (success, posts) = await ForumApi.TryGetPosts();
        Posts.AddRange(posts);
        Assert.IsTrue(success);
        Assert.IsTrue(Posts.Any());
    }

    [TestMethod]
    public async Task Test02GetPost()
    {
        foreach (var p in Posts)
        {
            var (success, post) = await ForumApi.TryGetPost(p.Id);
            Assert.IsTrue(success);
            Assert.IsTrue(post.IsValid);
        }
    }

}