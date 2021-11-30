using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest;

[TestClass]
public class NewsTest
{
    private static readonly IAsyncNewsApi NewsApi = new NewsWebApi();
    private static readonly List<ArticleInfo> News = new();

    [TestMethod]
    public async Task Test01GetNews()
    {
        var (success, news) = await NewsApi.TryGetNews();
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

}