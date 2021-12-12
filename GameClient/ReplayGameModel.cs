using GameModel;
using static System.Threading.Thread;
using static System.TimeSpan;

namespace GameClient;

public class ReplayGameModel : Model
{
    private readonly Game _gameData;

    public ReplayGameModel(Game game)
    {
        _gameData = game;
    }

    private void Act(TimeMarkedEvent e)
    {
        switch (e)
        {
            case MoveEvent move:
                Move(move);
                break;
            case EmoteEvent emote:
                Emote(emote);
                break;
            case TurnEvent turn:
                Turn(turn);
                break;
        }
    }

    public void Run()
    {
        Start(new GameStartEvent
        {
            Black = _gameData.Black,
            White = _gameData.White,
        });
        var previousTime = Zero;
        var events = new List<TimeMarkedEvent>();
        events.AddRange(_gameData.Moves);
        events.AddRange(_gameData.Emotions);
        events.AddRange(_gameData.Turns);
        var orderedActions = events.OrderBy(a => a.Time);
        foreach (var @event in orderedActions)
        {
            Sleep(@event.Time - previousTime);
            previousTime = @event.Time;
            Act(@event);
        }
        End(new GameEndEvent
        {
            Duration = _gameData.Duration,
            Start = _gameData.Start,
            Winner = _gameData.Winner,
            WinReason = _gameData.WinReason
        });

    }
}