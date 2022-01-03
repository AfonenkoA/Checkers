using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;
using WinFormsClient.Service.Interface;

namespace WinFormsClient.Presentation.Presenters;

public class CollectionPresenter : BasePresenter<ICollectionView, Credential>
{
    private readonly IUserService _repository;
    private Credential? _credential;
    public CollectionPresenter(IApplicationController controller,
        ICollectionView view,
        IUserService repository) :
        base(controller, view)
    {
        _repository = repository;
        View.OnBackToMenu += BackToMenu;
        View.OnAnimationSelect += SelectAnimation;
        View.OnCheckersSkinSelect += SelectCheckersSkin;
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
        var (_, collection) = await _repository.GetCollection(_credential);
        View.SetCollectionInfo(collection);
    }

    private async void SelectAnimation()
    {
        await _repository.SelectAnimation(_credential, View.SelectedAnimationsId);
        await UpdateCollectionInfo().ConfigureAwait(true);
    }

    private async void SelectCheckersSkin()
    {
        await _repository.SelectCheckers(_credential, View.SelectedCheckersId);
        await UpdateCollectionInfo().ConfigureAwait(true);
    }

    private void BackToMenu()
    {
        Controller.Run<MainMenuPresenter, Credential>(_credential);
        View.Close();
    }
}
