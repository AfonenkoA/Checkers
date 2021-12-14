using System.Collections.Concurrent;
using GameTransmission;

namespace GameServer.Match;

internal sealed class MatchMaker : IMatchMaker
{
    private static readonly GameAcknowledgeEvent GameAcknowledge = new();
    private readonly BlockingCollection<IPlayer> _players = new();
    private readonly MatchFactory _factory;

    internal MatchMaker(MatchFactory factory)
    {
        Task.Run(Maker);
        _factory = factory;
    }

    private void Maker()
    {
        while (true)
        {
            var black = _players.Take();
            var white = _players.Take();
            Task.Run(black.Listen);
            Task.Run(white.Listen);
            black.Send(GameAcknowledge);
            white.Send(GameAcknowledge);
            var match = _factory.CreateMatch(black, white);
            match.Run();
        }
    }

    public void AddPlayer(IPlayer sender) => _players.Add(sender);

    public void RemovePlayer(IPlayer sender)
    { }
}