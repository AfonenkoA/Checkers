using System.Linq;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest;

[TestClass]
public class UnitTest1
{
    private static readonly IAsyncItemApi ItemApi = new ItemWebApi();
    [TestMethod]
    public void GetAllItems()
    {
        var (success, items) = ItemApi.TryGetItems().GetAwaiter().GetResult();
        Assert.IsTrue(success);
        Assert.IsTrue(items.Any());
    }
}