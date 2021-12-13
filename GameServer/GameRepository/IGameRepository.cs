using GameModel;

namespace GameServer.GameRepository;

internal interface IGameRepository
{
    public void SaveGame(Game game);
}