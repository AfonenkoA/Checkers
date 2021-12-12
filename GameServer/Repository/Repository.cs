using Common.Entity;
using GameModel;
using static Common.CommunicationProtocol;

namespace GameServer.Repository;

internal sealed class Repository : IGameRepository, IPlayerRepository
{
    private static readonly Random Random = new();
    public Emotion GetEmotion(int id) => new();

    public void SaveGame(Game game) => File.WriteAllText($"{Random.NextInt64()}",Serialize(game));

    public PlayerInfo GetInfo(Credential c) => new();
}