using System.Collections.Generic;

namespace Checkers.Game.Server.Match;

internal sealed class MatchMaker : IMatchMaker
{
    private readonly Queue<IPlayer> _players = new();
    private readonly MatchFactory _factory;

    internal MatchMaker(MatchFactory factory) => _factory = factory;

    public void AddPlayer(IPlayer sender)
    {
        _players.Enqueue(sender);
        if (_players.Count < 2) return;
        var black = _players.Dequeue();
        var white = _players.Dequeue();
        _factory.CreateMatch(black,white).Run();
    }

    public void RemovePlayer(IPlayer sender)
    { }
}