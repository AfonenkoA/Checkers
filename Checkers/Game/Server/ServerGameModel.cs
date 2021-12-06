using System;
using Checkers.Game.Model;

namespace Checkers.Game.Server;

public sealed class ServerGameModel : GameModel
{
    internal IGameController BlackController { get; }
    internal IGameController WhiteController { get; }

    internal ServerGameModel()
    {
        BlackController = new GameController(this, Color.Black);
        WhiteController = new GameController(this, Color.White);
    }

    public override void Run()
    {
        throw new NotImplementedException();
    }
}