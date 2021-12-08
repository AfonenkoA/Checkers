namespace Checkers.Game.Server;

public interface IMatchMaker
{
    internal void AddPlayer(IPlayer sender);
    void RemovePlayer(IPlayer sender);
}