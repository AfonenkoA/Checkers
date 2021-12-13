using GameModel;

namespace Api.Interface;

public interface IAsyncGameApi
{
    public Task<bool> TryCreateGame(Game game);
    public Task<(bool, IdentifiableGame)> TryGetGame(int id);
    public Task<(bool, IEnumerable<GameInfo>)> TryGetLastGames();
}