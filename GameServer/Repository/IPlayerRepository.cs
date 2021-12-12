using Common.Entity;
using GameModel;

namespace GameServer.Repository;

public interface IPlayerRepository
{
    public PlayerInfo GetInfo(Credential c);
}