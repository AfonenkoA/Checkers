using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public sealed class ItemWebApi : WebApiBase, IAsyncItemApi
{
    public async Task<(bool, IEnumerable<ItemHash>)> TryGetItems()
    {
        var response = await Client.GetStringAsync(ItemRoute);
        var res = Deserialize<List<ItemHash>>(response);
        return res != null ? (res.All(i => i.IsValid), res) : (false, Enumerable.Empty<ItemHash>());
    }

    public async Task<(bool, ItemInfo)> TryGetItemInfo(int id)
    {
        var route = ItemRoute + $"/{id}";
        var response = await Client.GetStringAsync(route);
        var res = Deserialize<ItemInfo>(response);
        return res != null ? (res.IsValid, res) : (false, ItemInfo.Invalid);
    }

    public string GetItemImageUrl(int id) => ItemRoute + $"/{id}/img";

    public async Task<(bool, byte[])> TryGetItemImage(int id)
    {
        var route = ItemRoute + $"/{id}/img";
        var res = await Client.GetByteArrayAsync(route);
        return res.Any() ? (true, res) : (false, Array.Empty<byte>());
    }
}