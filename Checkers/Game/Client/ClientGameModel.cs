using System;
using System.Threading.Tasks;
using Checkers.Data.Entity;
using Checkers.Game.Model;

namespace Checkers.Game.Client;

public sealed class ClientGameModel : GameModel
{

    public ClientGameModel(Side color)
    { }

    public override Task Run()
    {
        throw new NotImplementedException();
    }
}