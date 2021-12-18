using Common.Entity;
using WinFormsClient.Model.Item;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IShopView : IView
{
    event Action OnBackToMenu;
    event Action ReloadShop;
    void SetShopInfo(Credential credential,IEnumerable <VisualAnimation> animations,
                        IEnumerable<VisualCheckersSkin> checkersSkin,
                        IEnumerable<VisualLootBox> lootBoxes);
}