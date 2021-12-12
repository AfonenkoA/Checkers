using Common.Entity;
using GameServer.GameRepository;
using GameTransmission;

namespace GameServer.Tcp;

public class PlayerFactory
{
    private readonly IPlayerRepository _repository;
    public PlayerFactory(IPlayerRepository repository) => _repository = repository;

    internal IPlayer CreatePlayer(Connection connection, Credential credential)
    {
        var p = new Player(_repository, connection, credential);
        Task.Run(p.Listen);
        return p;
    }
}