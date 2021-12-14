using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IItemShowPanelView : IView
{
    void SetPanelInfo(string title,string description);
}