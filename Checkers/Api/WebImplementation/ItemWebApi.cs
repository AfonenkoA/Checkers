using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public sealed class ItemWebApi : WebApiBase, IAsyncItemApi
{
    public Task<(bool, IEnumerable<ItemHash>)> TryGetItems() =>
        Client.GetStringAsync(ItemRoute)
            .ContinueWith(task => Deserialize<List<ItemHash>>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (res.All(i => i.IsValid), res) : (false,Enumerable.Empty<ItemHash>());
            });

    public Task<(bool, ItemInfo)> TryGetItemInfo(int id) =>
        Client.GetStringAsync(ItemRoute + $"/{id}")
            .ContinueWith(task => Deserialize<ItemInfo>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (res.IsValid, res) : (false, ItemInfo.Invalid);
            });

    public string GetItemImageUrl(int id) => ItemRoute + $"/{id}/img";

    public Task<(bool,byte[])> TryGetItemImage(int id) =>
        Client.GetByteArrayAsync(ItemRoute + $"/{id}/img")
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res.Any()?(true, res):(false,Array.Empty<byte>());
            });
}