using System.Collections.Generic;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTest;

[TestClass]
public class ItemTest
{
    private static readonly IAsyncItemApi ItemApi = new ItemWebApi();

    private static readonly List<ItemHash> ItemHashes = new();
    [TestMethod]
    public async Task GetAllItems()
    {
        var (success, items) = await ItemApi.TryGetItems();
        ItemHashes.AddRange(items);
        Assert.IsTrue(success); 
    }

    [TestMethod]
    public async Task GetItemInfo()
    {
        foreach (var itemHash in ItemHashes)
        {
            var (success, _) = await ItemApi.TryGetItemInfo(itemHash.Id);
            Assert.IsTrue(success);
        }
    }
}