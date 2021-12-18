using Api.Interface;
using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;
using WinFormsClient.Repository.Interface;
using static System.Globalization.CultureInfo;

namespace WinFormsClient.Presentation.Presenters;

public class ProfilePresenter : BasePresenter<IProfileView, Credential>
{
    private readonly IUserRepository _repository;
    private Credential _credential;

    public ProfilePresenter(IApplicationController controller, IProfileView view, IUserRepository repository
    ) : base(controller, view)
    {
        _repository = repository;
    }
    public override void Run(Credential argument)
    {
        _credential = argument;
        Task.Run(UpdateUserInfo).Wait();
        View.Show();
    }
    private async Task UpdateUserInfo()
    {
        var (_, user) = await _repository.GetSelf(_credential);
        View.SetUserInfo(user.Nick,user.SocialCredit.ToString(), user.LastActivity.ToString(CurrentCulture),user.Image);
    }
}