using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest;

[TestClass]
public class ForumTest
{
    private static readonly IAsyncForumApi ForumApi = new ForumWebApi();
    private static readonly IAsyncResourceService ResourceService = new AsyncResourceWebApi();
    private static readonly List<PostInfo> Posts = new();
    private static readonly Credential Credential = new() { Login = "redactor", Password = "redactor" };
    private const string Title = "Grape Title";
    private const string Content = "Grape Content";
    private const string NewTitle = "New Grape Title";
    private const string NewContent = "New Grape Content";
    private const string Ext = "jpg";
    private const string Image1 = @"Resource\Apple.jpg";
    private const string Image2 = @"Resource\Grape.jpg";


    private static Task<(bool, IEnumerable<PostInfo>)> GetPosts() => ForumApi.TryGetPosts();

    [TestMethod]
    public async Task Test01GetPosts()
    {
        var (success, posts) = await GetPosts();
        Assert.IsTrue(success);
        Posts.AddRange(posts);
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

    [TestMethod]
    public async Task Test03CreatePost()
    {
        var (success, id) = await ResourceService.TryUploadFile(Credential, await File.ReadAllBytesAsync(Image1), Ext);
        Assert.IsTrue(success);
        var data = new PostCreationData { Title = Title, Content = Content, PictureId = id };
        success = await ForumApi.CreatePost(Credential, data);
        Assert.IsTrue(success);
    }


    private static async Task<int> GetId()
    {
        var (_, posts) = await GetPosts();
        return (from info in posts where info.Title == Title select info.Id).FirstOrDefault();
    }

    [TestMethod]
    public async Task Test04UpdateContent()
    {
        var id = await GetId();
        var success = await ForumApi.UpdateContent(Credential, id, NewContent);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test05UpdatePicture()
    {
        var id = await GetId();
        var (success, pic) = await ResourceService.TryUploadFile(Credential, await File.ReadAllBytesAsync(Image2), Ext);
        Assert.IsTrue(success);
        success = await ForumApi.UpdatePicture(Credential, id, pic);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test06UpdateTitle()
    {
        var id = await GetId();
        var success = await ForumApi.UpdateTitle(Credential, id, NewTitle);
        Assert.IsTrue(success);
    }



}