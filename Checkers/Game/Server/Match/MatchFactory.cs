using Checkers.Game.Server.Repository;

namespace Checkers.Game.Server.Match;

public class MatchFactory
{
    private readonly IGameRepository _repository;
    internal MatchFactory(IGameRepository repository) => _repository = repository;

    internal Match CreateMatch(IPlayer black, IPlayer white) =>
        new SavedMatch(_repository, black, white);
}