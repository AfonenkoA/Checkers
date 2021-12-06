using System;
using Checkers.Game.Model;

namespace Checkers.Game.Client;

public sealed class ClientGameModel : GameModel
{
    public IGameController Controller { get; }

    public ClientGameModel(Color color)
    {
        Controller = new GameController(this,color);
    }

    public override void Run()
    {
        throw new NotImplementedException();
    }
}