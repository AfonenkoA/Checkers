using Common.Entity;

namespace GameServer.Repository;

internal interface IEmotionRepository
{
    public Emotion GetEmotion(int id);
}