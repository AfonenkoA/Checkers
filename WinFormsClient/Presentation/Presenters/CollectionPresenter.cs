using Api.Interface;
using Common.Entity;
using WinFormsClient.Presentation.Common;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient.Presentation.Presenters;

    public class CollectionPresenter : BasePresenter<ICollectionView, Credential>
    {
        private readonly IAsyncUserApi _userApi;
        private Credential? _credential;
        public CollectionPresenter(IApplicationController controller, ICollectionView view, IAsyncUserApi userApi) :
            base(controller, view)
        {
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
            var checkers = user.CheckerSkins;
            var animations = user.Animations;
            

            View.SetCollectionInfo(animations, checkers);
        }

        private void BackToMenu()
        {
            Controller.Run<MainMenuPresenter, Credential>(_credential);
            View.Close();
        }
    }
