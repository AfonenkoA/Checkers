using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkers.Game.Server;

public interface IMatchMaker
{
    internal void AddPlayer(IAuthorisedPlayer sender);
    void RemovePlayer(IAuthorisedPlayer sender);
}

public sealed class MatchMaker
{
    private readonly Queue<IAuthorisedPlayer> _players = new();

    internal void AddPlayer(IAuthorisedPlayer sender)
    {
        _players.Enqueue(sender);
        if (_players.Count < 2) return;
        var black = _players.Dequeue();
        var white = _players.Dequeue();
        var _ = Task.Run(new Match(black, white).Run);
    }

    internal void RemovePlayer(Player sender)
    {

    }

}