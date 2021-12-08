namespace Checkers.Game.Server.Match;

public interface IMatchMaker
{
    public void AddPlayer(IPlayer sender);
    public void RemovePlayer(IPlayer sender);
}