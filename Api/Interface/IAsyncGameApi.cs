using GameModel;

namespace Api.Interface;

public interface IAsyncGameApi
{
    public Task<bool> CreateGame(Game game);
    public Task<(bool,IdentifiableGame)> GetGame(int id);
}