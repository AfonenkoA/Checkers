using GameServer.GameRepository;

namespace GameServer.Match;

internal sealed class MatchFactory
{
    private readonly IGameRepository _repository;
    internal MatchFactory(IGameRepository repository) => _repository = repository;

    internal MatchModel CreateMatch(IPlayer black, IPlayer white) =>
        new SavedMatch(_repository, black, white);
}