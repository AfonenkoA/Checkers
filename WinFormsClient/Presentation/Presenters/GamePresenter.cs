using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;
using WinFormsClient.Service.Interface;

namespace WinFormsClient.Presentation.Presenters;

internal class GamePresenter : BasePresenter<IGameView, Credential>
{
    public async Task SetGameInfo()
    {
        if (_credential == null) throw new ArgumentException("Null credential");
        var (_, self) = await _repository.GetSelf(_credential);
        var (_, bot) = await _repository.GetUser(4);
        View.SetGameInfo(self,bot);
    }

    private Credential? _credential;
    private readonly IUserService? _repository;

    public GamePresenter(IApplicationController controller,
        IGameView view,
        IUserService? repository) :
        base(controller, view)
    {
        _repository = repository;
    }
    public override void Run(Credential argument)
    {
        _credential = argument;
        Task.Run(SetGameInfo).Wait();
        View.Show();
    }

}