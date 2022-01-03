using WinFormsClient.Model;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient.Presentation.Views;

internal interface IGameView : IView
{
    void SetGameInfo(Self self, User enemy);
}