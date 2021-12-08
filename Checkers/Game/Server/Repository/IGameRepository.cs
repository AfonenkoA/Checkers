using Checkers.Game.Model;

namespace Checkers.Game.Server.Repository;

internal interface IGameRepository : IEmotionRepository
{
    public void SaveGame(GameData game);
}