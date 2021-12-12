using Api.Interface;
using GameModel;

namespace Api.WebImplementation;

internal class GameWebApi : IAsyncGameApi
{
    public Task<bool> CreateGame(Game game)
    {
        throw new NotImplementedException();
    }

    public Task<(bool, IdentifiableGame)> GetGame(int id)
    {
        throw new NotImplementedException();
    }
}