using Checkers.Data.Entity;

namespace Checkers.Game.Server;

public sealed class Server
{
    private readonly IPlayers _players;
    private readonly IMatchMaker _matchMaker;
    public Server(IPlayers players,IMatchMaker matchMaker)
    {
        _players = players;
        _matchMaker = matchMaker;
    }

    private static bool Authenticate(Credential c) => true;

    public async void Run()
    {
        await foreach (var player in _players)
        {
            if(!Authenticate(player.Credential))
                player.Dispose();
            //player.OnGameRequest += _matchMaker.AddPlayer;
            //player.OnDisconnect += _matchMaker.RemovePlayer;
        }
    }
}