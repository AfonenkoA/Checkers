using Checkers.Data.Entity;
using Checkers.Game.Model;

namespace Checkers.Game.Server.Repository;

public interface IPlayerRepository
{
    public PlayerInfo GetInfo(Credential c);
}