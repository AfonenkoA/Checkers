using Api.Interface;
using GameModel;

namespace Api.WebImplementation;

internal class GameWebApi : IAsyncGameApi
{
    public Task<bool> TryCreateGame(Game game)
    {
        throw new NotImplementedException();
    }

    public Task<(bool, IdentifiableGame)> TryGetGame(int id)
    {
        throw new NotImplementedException();
    }

    public Task<(bool, IEnumerable<GameInfo>)> TryGetLastGames()
    {
        throw new NotImplementedException();
    }
}