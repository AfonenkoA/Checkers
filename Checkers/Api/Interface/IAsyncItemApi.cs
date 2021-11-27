using System.Collections.Generic;
using System.Threading.Tasks;
using Checkers.Data.Entity;

namespace Checkers.Api.Interface;

public interface IAsyncItemApi
{
    Task<(bool,IEnumerable<ItemHash>)> TryGetItems();
    Task<(bool,ItemInfo)> TryGetItemInfo(int id);
    string GetItemImageUrl(int id);
    Task<(bool, byte[])> TryGetItemImage(int id);
}