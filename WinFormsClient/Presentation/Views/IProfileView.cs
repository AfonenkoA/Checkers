using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views
{
    public interface IProfileView:IView
    {
        void SetUserInfo(string nickname, string socialCredit,string lastactivity);
    }
}
