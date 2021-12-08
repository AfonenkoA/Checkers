using Checkers.Data.Entity;

namespace Checkers.Game.Server;

internal interface IEmotionRepository
{
    public Emotion GetEmotion(int id);
}