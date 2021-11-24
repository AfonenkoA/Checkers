using System.Collections.Generic;
using Checkers.Data.Entity;

namespace Checkers.Data.Repository.Interface
{
    public interface IItemRepository
    {
        IEnumerable<ItemHash> GetItems();
        ItemInfo GetItemInfo(int id);
        (byte[],string) GetItemImage(int id);
    }
}
