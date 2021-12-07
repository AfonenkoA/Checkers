using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Checkers.Game.Transmission;
using static System.Net.Sockets.TcpListener;

namespace Checkers.Game.Server;

public interface IPlayers : IAsyncEnumerable<Player>
{}

public interface IConnection { }
public interface IConnectionProvider { }
public interface IPlayer { }
public interface IAuthorisedPlayer : IPlayer {} 

public sealed class TcpPlayerProvider : IPlayers
{
    private readonly TcpListener _listener;
    public TcpPlayerProvider(int port)
    {
        _listener = Create(port);
        _listener.Start();
    }

    public async IAsyncEnumerator<Player> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        while (true)
        {
            Connection connection = new(await _listener.AcceptTcpClientAsync(cancellationToken));
            var action = await connection.ReceiveObject<ConnectRequestAction>();
            if (action == null)
            {
                connection.Dispose();
                continue;
            }
            Player player = new(connection,action.Credential);
            yield return player;
        }
    }
}