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
public class NewsTest
{
    private static readonly IAsyncNewsApi NewsApi = new NewsWebApi();
    private static readonly IAsyncResourceService ResourceService = new AsyncResourceWebApi();
    private static readonly List<ArticleInfo> News = new();
    private static readonly Credential Credential = new(){Login = "redactor", Password = "redactor"};
    private const string Title = "Apple Title";
    private const string Abstract = "Apple Abstract";
    private const string Content = "Apple Content";
    private const string NewTitle = "New Apple Title";
    private const string NewAbstract = "New Apple Abstract";
    private const string NewContent = "New Apple Content";
    private const string Ext = "jpg";
    private const string Image1 = @"Resource\Apple.jpg";
    private const string Image2 = @"Resource\Grape.jpg";


    private static Task<(bool,IEnumerable<ArticleInfo>)> GetNews() => NewsApi.TryGetNews();

    [TestMethod]
    public async Task Test01GetNews()
    {
        var (success, news) = await GetNews();
        Assert.IsTrue(success);
        News.AddRange(news);
        Assert.IsTrue(News.Any());
    }

    [TestMethod]
    public async Task Test02GetPost()
    {
        foreach (var a in News)
        {
            var (success, article) = await NewsApi.TryGetArticle(a.Id);
            Assert.IsTrue(success);
            Assert.IsTrue(article.IsValid);
        }
    }

    [TestMethod]
    public async Task Test03CreateArticle()
    {
        var (success,id) = await ResourceService.TryUploadFile(Credential, await File.ReadAllBytesAsync(Image1), Ext);
        Assert.IsTrue(success);
        var data = new ArticleCreationData {Title = Title, Abstract = Abstract, Content = Content, PictureId = id};
        success = await NewsApi.CreateArticle(Credential, data);
        Assert.IsTrue(success);
    }


    private static async Task<int> GetId()
    {
        var (_, news) = await GetNews();
        return (from info in news where info.Title == Title select info.Id).FirstOrDefault();
    }

    [TestMethod]
    public async Task Test04UpdateAbstract()
    {
        var id = await GetId();
        var success = await NewsApi.UpdateAbstract(Credential, id, NewAbstract);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test05UpdateContent()
    {
        var id = await GetId();
        var success = await NewsApi.UpdateContent(Credential, id, NewContent);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test06UpdatePicture()
    {
        var id = await GetId();
        var (success, pic) = await ResourceService.TryUploadFile(Credential, await File.ReadAllBytesAsync(Image2), Ext);
        Assert.IsTrue(success);
        success = await NewsApi.UpdatePicture(Credential, id, pic);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test07UpdateTitle()
    {
        var id = await GetId();
        var success = await NewsApi.UpdateTitle(Credential, id, NewTitle);
        Assert.IsTrue(success);
    }
}