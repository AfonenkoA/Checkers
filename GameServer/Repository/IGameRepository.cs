using GameModel;

namespace GameServer.Repository;

internal interface IGameRepository : IEmotionRepository
{
    public void SaveGame(Game game);
}