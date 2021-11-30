using System.Collections.Generic;
using System.Linq;
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
    public async Task Test01GetAllItems()
    {
        var (success, items) = await ItemApi.TryGetItems();
        ItemHashes.AddRange(items);
        Assert.IsTrue(success); 
        Assert.IsTrue(ItemHashes.Any());
    }

    [TestMethod]
    public async Task Test02GetItemInfo()
    {
        foreach (var itemHash in ItemHashes)
        {
            var (success, _) = await ItemApi.TryGetItemInfo(itemHash.Id);
            Assert.IsTrue(success);
        }
    }
}