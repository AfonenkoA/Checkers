using Common.Entity;
using GameServer.Repository;
using GameTransmission;

namespace GameServer.Tcp;

public class PlayerFactory
{
    private readonly IPlayerRepository _repository;
    public PlayerFactory(IPlayerRepository repository) => _repository = repository;

    internal Player CreatePlayer(Connection connection, Credential credential)
    {
        var p = new Player(_repository, connection, credential);
        Task.Run(p.Listen);
        return p;
    }
}