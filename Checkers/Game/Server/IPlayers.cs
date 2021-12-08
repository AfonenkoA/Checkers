using System.Collections.Generic;

namespace Checkers.Game.Server;

public interface IPlayers : IAsyncEnumerable<IPlayer>
{}