using WinFormsClient.Model;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IShopView : IView
{
    event Action OnBackToMenu;
    void SetShopInfo(IEnumerable<VisualAnimation> animations,
                        IEnumerable<VisualCheckersSkin> checkersSkin,
                        IEnumerable<VisualLootBox> lootBoxes);
}