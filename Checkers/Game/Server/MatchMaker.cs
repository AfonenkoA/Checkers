using System.Collections.Generic;

namespace Checkers.Game.Server;

public sealed class MatchMaker
{
    private readonly Queue<IPlayer> _players = new();

    internal void AddPlayer(IPlayer sender)
    {
        _players.Enqueue(sender);
        if (_players.Count < 2) return;
        var black = _players.Dequeue();
        var white = _players.Dequeue();
    }

    internal void RemovePlayer(IPlayer sender)
    {

    }

}