using Common.Entity;
using WinFormsClient.Model;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface ICollectionView : IView
{
    event Action BackToMenu;
    void SetCollectionInfo(IEnumerable<VisualAnimation> animations,
        IEnumerable<VisualCheckersSkin> checkers);
}