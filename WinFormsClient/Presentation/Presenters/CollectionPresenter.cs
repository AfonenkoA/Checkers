using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;
using WinFormsClient.Repository.Interface;

namespace WinFormsClient.Presentation.Presenters;

public class CollectionPresenter : BasePresenter<ICollectionView, Credential>
{
    private readonly IUserRepository _repository;
    private Credential? _credential;
    public CollectionPresenter(IApplicationController controller,
        ICollectionView view,
        IUserRepository repository) :
        base(controller, view)
    {
        _repository = repository;
        View.BackToMenu += BackToMenu;
    }
    public override void Run(Credential argument)
    {
        _credential = argument;
        Task.Run(UpdateCollectionInfo).Wait();
        View.Show();
    }
    private async Task UpdateCollectionInfo()
    {
        if (_credential == null) throw new ArgumentException("Null credential");
        var (_,user) = await _repository.GetSelf(_credential);
        View.SetCollectionInfo(user.Animations, user.CheckersSkins);
    }

    private void BackToMenu()
    {
        Controller.Run<MainMenuPresenter, Credential>(_credential);
        View.Close();
    }
}
