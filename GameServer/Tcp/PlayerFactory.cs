using Common.Entity;
using GameServer.GameRepository;
using GameTransmission;

namespace GameServer.Tcp;

public sealed class PlayerFactory
{
    private readonly IPlayerRepository _repository;
    internal PlayerFactory(IPlayerRepository repository) => _repository = repository;

    internal IPlayer CreatePlayer(Connection connection, Credential credential)
    {
        var p = new Player(_repository, connection, credential);
        Task.Run(p.ListenClient);
        return p;
    }
}