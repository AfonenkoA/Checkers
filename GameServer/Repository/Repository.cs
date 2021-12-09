using Common.Entity;
using GameModel;
using static System.DateTime;
using static Common.CommunicationProtocol;

namespace GameServer.Repository;

internal sealed class Repository : IGameRepository, IPlayerRepository
{
    public Emotion GetEmotion(int id) => new();

    public void SaveGame(Game game) => File.WriteAllText($"{Now}",Serialize(game));

    public PlayerInfo GetInfo(Credential c) => new();
}