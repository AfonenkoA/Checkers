using WinFormsClient.Model;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IShopView : IView
{
    event Action OnBuyCheckersSkin;
    event Action OnBuyAnimation;
    event Action OnBuyLootBox;
    event Action OnBackToMenu;
    public int  CheckersSkinId { get; }
    public int AnimationId { get; }
    public int LootBoxId { get; }
    void SetShopInfo(Shop shop);
}