using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

public interface IShopView:IView
{
    void SetShopInfo(List<Control> l1, List<Control> l2, List<Control> l3);
}