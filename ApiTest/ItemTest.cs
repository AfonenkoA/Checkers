using System.Linq;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest;

[TestClass]
public class ItemTest
{
    private static readonly IAsyncItemApi ItemApi = new ItemWebApi();
    [TestMethod]
    public async Task GetAllItems()
    {
        var (success, _) = await ItemApi.TryGetItems();
        Assert.IsTrue(success); 
    }

    [TestMethod]
    public async Task GetItemInfo()
    {
        var (success,info) = await ItemApi.TryGetItemInfo(1);
        Assert.IsTrue(success);
        Assert.IsTrue(info.IsValid);
    }
}