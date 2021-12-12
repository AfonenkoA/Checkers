using Common.Entity;

namespace GameServer.GameRepository;

internal interface IEmotionRepository
{
    public Emotion GetEmotion(int id);
}