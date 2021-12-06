using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkers.Game.Server.Match;

public sealed class MatchMaker
{
    private readonly Queue<Player> _players = new Queue<Player>();

    internal void AddPlayer(Player sender)
    {
        _players.Enqueue(sender);
        if (_players.Count < 2) return;
        var black = _players.Dequeue();
        var white = _players.Dequeue();
        var _ = Task.Run(new Match(black, white).Start);
    }

}