using Api.Interface;
using Common.Entity;
using WinFormsClient.Model;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

    public class CollectionPresenter : BasePresenter<ICollectionView, Credential>
    {
        private readonly IAsyncUserApi _userApi;
        private readonly ResourceManager _resourceManager;
        private Credential? _credential;
        public CollectionPresenter(IApplicationController controller,
            ICollectionView view,
            IAsyncUserApi userApi,
            ResourceManager manager) :
            base(controller, view)
        {
            _resourceManager = manager;
            _userApi = userApi;
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
            var (_, user) = await _userApi.TryGetSelf(_credential ?? new Credential());
            var checkers = user.CheckerSkins.ToList();
            var animations = user.Animations.ToList();
            await _resourceManager.PreLoad(checkers);
            await _resourceManager.PreLoad(animations);

            View.SetCollectionInfo(animations.Select(a => new VisualAnimation(a, _resourceManager.Get(a))),
                checkers.Select(c => new VisualCheckersSkin(c, _resourceManager.Get(c))));
        }

        private void BackToMenu()
        {
            Controller.Run<MainMenuPresenter, Credential>(_credential);
            View.Close();
        }
    }
