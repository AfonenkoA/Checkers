using Common.Entity;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IShopView : IView
{
    event Action BackToMenu;
    void SetShopInfo(IEnumerable<(Animation,Image)> l1, IEnumerable<(LootBox,Image)> l2, IEnumerable<(CheckersSkin,Image)> l3);
}