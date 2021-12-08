using Checkers.Game.Model;

namespace Checkers.Game.Server;

internal interface IGameRepository : IEmotionRepository
{
    public void SaveGame(GameData game);
}