using System.Threading.Tasks;
using Checkers.Game.Model;
using EmoteEvent = Checkers.Game.Transmission.EmoteEvent;
using GameEndEvent = Checkers.Game.Transmission.GameEndEvent;
using GameStartEvent = Checkers.Game.Transmission.GameStartEvent;
using MoveEvent = Checkers.Game.Transmission.MoveEvent;

namespace Checkers.Game.Server;

public interface IRepository
{

}


public sealed class Match : GameModel
{
    private readonly IAuthorisedPlayer _black;
    private readonly IAuthorisedPlayer _white;

    public Match(IAuthorisedPlayer black, IAuthorisedPlayer white)
    {

    }

    public void Log(EmoteEvent e)
    {

    }

    public void Log(GameStartEvent e)
    {

    }

    public void Log(GameEndEvent e)
    {

    }

    public void Log(MoveEvent e)
    {

    }

    public override Task Run()
    {
        throw new System.NotImplementedException();
    }
}