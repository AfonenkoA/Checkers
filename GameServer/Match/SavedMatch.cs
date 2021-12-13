using GameModel;
using GameServer.GameRepository;

namespace GameServer.Match;

internal sealed class SavedMatch : MatchModel
{
    private readonly IGameRepository _repository;
    private readonly List<MoveEvent> _moves = new();
    private readonly List<EmoteEvent> _emotes = new();
    private readonly List<TurnEvent> _turns = new();
    private GameStartEvent? _gameStartEvent;

    private void Save(GameStartEvent e) => _gameStartEvent = e;

    private void Save(GameEndEvent e)
    {
        if (_gameStartEvent == null) throw new Exception("Game save exception");
        _repository.SaveGame(new Game
        {
            Black = Black.PlayerData,
            White = White.PlayerData,
            Duration = e.Duration,
            Emotions = _emotes,
            Moves = _moves,
            Turns = _turns,
            Start = _gameStartEvent.Start,
            Winner = e.Winner,
            WinReason = e.WinReason
        });
        Unsubscribe();
    }

    private void Save(TurnEvent e) => _turns.Add(e);

    private void Save(MoveEvent e) => _moves.Add(e);

    private void Save(EmoteEvent e) => _emotes.Add(e);

    private void Subscribe()
    {
        OnGameStart += Save;
        OnGameEnd += Save;
        OnTurn += Save;
        OnEmote += Save;
        OnMove += Save;
    }

    private void Unsubscribe()
    {
        OnGameStart -= Save;
        OnGameEnd -= Save;
        OnTurn -= Save;
        OnEmote -= Save;
        OnMove -= Save;
    }

    internal SavedMatch(IGameRepository repository, IPlayer black, IPlayer white) :
        base(black, white)
    {
        _repository = repository;
        Subscribe();
    }
}