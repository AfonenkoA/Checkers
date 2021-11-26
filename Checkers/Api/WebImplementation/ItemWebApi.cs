using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Data.Entity;
using static System.Text.Json.JsonSerializer;

namespace Checkers.Api.WebImplementation;

public sealed class ItemWebApi : WebApiBase, IAsyncItemApi
{
    public Task<(bool, IEnumerable<ItemHash>)> TryGetItems() =>
        Client.GetStringAsync(ItemRoute)
            .ContinueWith(task => Deserialize<IEnumerable<ItemHash>>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (true, res) : (false, Enumerable.Empty<ItemHash>());
            });

    public Task<(bool, ItemInfo)> TryGetItemInfo(int id) =>
        Client.GetStringAsync(ItemRoute + $"/{id}")
            .ContinueWith(task => Deserialize<ItemInfo>(task.Result))
            .ContinueWith(task =>
            {
                var res = task.Result;
                return res != null ? (true, res) : (false, ItemInfo.Invalid);
            });

    public string GetItemImageUrl(int id) => ItemRoute + $"/{id}/img";

    public Task<byte[]> GetItemImage(int id) =>
        Client.GetByteArrayAsync(ItemRoute + $"/{id}/img");
}