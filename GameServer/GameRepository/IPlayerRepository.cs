using Common.Entity;
using GameModel;

namespace GameServer.GameRepository;

public interface IPlayerRepository
{
    public PlayerInfo GetInfo(Credential c);
}