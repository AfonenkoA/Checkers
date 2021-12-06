using System.Threading.Tasks;
using Checkers.Game.Server.Transmission;

namespace Checkers.Game.Server.Match;

class Match
{
    private readonly ServerGameModel _model;
    private readonly Player _black;
    private readonly Player _white;
    public Match(Player black, Player white)
    {
        _black = black;
        _white = white;
        _model = new ServerGameModel();
        black.AddListener(_model.BlackController);
        white.AddListener(_model.WhiteController);
        _model.AddListener(white);
        _model.AddListener(black);
        _model.AddListener(this);
    }

    public void Start() => Task.Run(_model.Run);


    public void Log(EmoteEvent e)
    {

    }
    public void Log(GameStartEvent e)
    {

    }

    public void Log(GameEndEvent e)
    {
        _model.RemoveListener(_black);
        _model.RemoveListener(_white);
        _model.RemoveListener(this);
    }

    public void Log(MoveEvent e)
    {

    }
}