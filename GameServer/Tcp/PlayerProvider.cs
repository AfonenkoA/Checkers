using System.Net.Sockets;
using GameTransmission;
using static System.Net.Sockets.TcpListener;

namespace GameServer.Tcp;

internal sealed class PlayerProvider : IPlayers
{
    private readonly TcpListener _listener;
    private readonly PlayerFactory _factory;

    internal PlayerProvider(PlayerFactory factory, int port)
    {
        _listener = Create(port);
        _factory = factory;
        _listener.Start();
    }

    public async IAsyncEnumerator<IPlayer> GetAsyncEnumerator(CancellationToken cancellationToken = new())
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
            await connection.Transmit(new ConnectAcknowledgeEvent());
            yield return _factory.CreatePlayer(connection, action.Credential);
        }
    }
}