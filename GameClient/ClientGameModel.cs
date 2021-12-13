using GameModel;
using static System.Threading.Tasks.Task;
using static GameClient.IPlayer;

namespace GameClient;

public sealed class ClientGameModel : InteroperableModel
{
    public event YourSideEventHandler? OnYourSide;

    private readonly IPlayer _player;

    public ClientGameModel(IPlayer player)
    {
        _player = player;
        Subscribe();
        Run(player.Listen);
    }

    public override void Move(MoveAction a) => _player.Send(a);

    public override void Emote(EmoteAction a) => _player.Send(a);

    public override void Surrender(SurrenderAction a) => _player.Send(a);
    private void YourSide(YourSideEvent e) => OnYourSide?.Invoke(e);


    private void Subscribe()
    {
        _player.OnMove += Move;
        _player.OnEmote += Emote;
        _player.OnException += Exception;
        _player.OnYouSide += YourSide;
        _player.OnGameStart += Start;
        _player.OnGameEnd += End;
        _player.OnTurn += Turn;
    }

    private new void End(GameEndEvent e)
    {
        base.End(e);
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        _player.OnMove -= Move;
        _player.OnEmote -= Emote;
        _player.OnException -= Exception;
        _player.OnYouSide -= YourSide;
        _player.OnGameStart -= Start;
        _player.OnGameEnd -= End;
        _player.OnTurn -= Turn;
    }

}