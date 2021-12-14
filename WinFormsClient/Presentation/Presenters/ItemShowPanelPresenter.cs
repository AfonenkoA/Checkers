using Api.Interface;
using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

public class ItemShowPanelPresenter : BasePresenter<IItemShowPanelView,DetailedItem>
{
    
    private  DetailedItem _item;
    public ItemShowPanelPresenter(IApplicationController controller, IItemShowPanelView view):base(controller,view)
    {
        

    }


    public override void Run(DetailedItem argument)
    {
        _item = argument;
        UpdatePanelInfo(_item);
        View.Show();

    }
    private void UpdatePanelInfo(DetailedItem item)
    {
        _item = item;
        View.SetPanelInfo(_item.Name,_item.Detail);
    }
}