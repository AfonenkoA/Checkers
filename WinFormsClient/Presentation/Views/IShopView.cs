using Common.Entity;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IShopView : IView
{
    void SetShopInfo(IEnumerable<Animation> l1, IEnumerable<LootBox> l2, IEnumerable<CheckersSkin> l3);
}