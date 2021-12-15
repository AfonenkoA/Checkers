using Common.Entity;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface ICollectionView : IView
{
    event Action BackToMenu;
    void SetCollectionInfo(IEnumerable<Animation> l1, IEnumerable<CheckersSkin> l2);
}