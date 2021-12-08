using Checkers.Game.Server.Match;

namespace Checkers.Game.Server.Tcp;

public static class TcpServer
{
    public static Server CreateServer(int port)
    {
        var repository = new Repository.Repository();
        var playerFactory = new PlayerFactory(repository);
        var provider = new PlayerProvider(playerFactory, port);
        var matchFactory = new MatchFactory(repository);
        var matchMaker = new MatchMaker(matchFactory);
        return new Server(provider, matchMaker);
    }
}