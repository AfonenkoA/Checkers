using GameModel;

namespace GameServer.GameRepository;

internal interface IGameRepository : IEmotionRepository
{
    public void SaveGame(Game game);
}