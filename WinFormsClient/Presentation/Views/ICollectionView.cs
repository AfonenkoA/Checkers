using WinFormsClient.Model;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface ICollectionView : IView
{
    event Action OnBackToMenu;
    event Action OnAnimationSelect;
    event Action OnCheckersSkinSelect;
    public int SelectedCheckersId { get; }
    public int SelectedAnimationsId { get; }
    void SetCollectionInfo(Collection collection);
}