using Checkers.Data.Entity;

namespace Checkers.Game.Server.Repository;

internal interface IEmotionRepository
{
    public Emotion GetEmotion(int id);
}