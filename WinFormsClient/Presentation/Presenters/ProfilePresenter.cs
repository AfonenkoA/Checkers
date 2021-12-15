using Api.Interface;
using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;
using static System.Globalization.CultureInfo;

namespace WinFormsClient.Presentation.Presenters
{
    public class ProfilePresenter : BasePresenter<IProfileView, Credential>
    {
        private readonly IAsyncUserApi _userApi ;
        private Credential _credential;

        public ProfilePresenter(IApplicationController controller, IProfileView view,IAsyncUserApi userApi) : base(controller, view)
        {
            _userApi = userApi;
        }
        public override void Run(Credential argument)
        {
            _credential = argument;
           Task.Run(UpdateUserInfo).Wait();
            View.Show();
        }
        private async Task UpdateUserInfo()
        {
            var (_, user) = await _userApi.TryGetSelf(_credential);
            View.SetUserInfo(user.Nick,user.SocialCredit.ToString(), user.LastActivity.ToString(CurrentCulture));
        }
    }
}
