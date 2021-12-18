using Common.Entity;
using WinFormsClient.Model.Item;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IShopView : IView
{
    event Action OnBuyCheckersSkin;
    event Action OnBuyAnimation;
    event Action OnBuyLootBox;
    event Action OnBackToMenu;
    event Action ReloadShop;
    public int  CheckersSkinId { get; }
    public int AnimationId { get; }
    public int LootBoxId { get; }
    void SetShopInfo(Credential credential,IEnumerable <VisualAnimation> animations,
                        IEnumerable<VisualCheckersSkin> checkersSkin,
                        IEnumerable<VisualLootBox> lootBoxes);
}