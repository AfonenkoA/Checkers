using System.Threading.Tasks;
using Checkers.Game.Server.Match;

namespace Checkers.Game.Server;

public sealed class Server
{
    private readonly IPlayers _players;
    private readonly IMatchMaker _matchMaker;

    internal Server(IPlayers players, IMatchMaker matchMaker)
    {
        _players = players;
        _matchMaker = matchMaker;
    }

    public async Task Run()
    {
        await foreach (var player in _players)
        {
            player.OnGameRequest += _matchMaker.AddPlayer;
            player.OnDisconnect += _matchMaker.RemovePlayer;
        }
    }
}