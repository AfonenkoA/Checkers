using Common.Entity;
using GameModel;
using GameTransmission;
using static GameTransmission.Message;
using static GameClient.IPlayer;

namespace GameClient;

public sealed class Client : IPlayer, IClient, IDisposable
{
    private readonly Connection _connection;

    private readonly ConnectRequestAction _connectionRequest;
    private readonly GameRequestAction _gameRequest = new();
    private readonly DisconnectRequestAction _disconnectRequest = new();

    public Client(Connection connection, Credential credential)
    {
        _connection = connection;
        _connectionRequest = new ConnectRequestAction { Credential = credential };
    }


    public async void Listen()
    {
        while (true)
        {
            var json = await _connection.ReceiveString();
            if (json == null) continue;
            var message = FromString(json);
            if (message == null) continue;
            switch (message.Type)
            {
                case nameof(TurnEvent):
                    var turn = message.GetAs<TurnEvent>();
                    OnTurn?.Invoke(turn);
                    break;
                case nameof(MoveEvent):
                    var move = message.GetAs<MoveEvent>();
                    OnMove?.Invoke(move);
                    break;
                case nameof(EmoteEvent):
                    var emote = message.GetAs<EmoteEvent>();
                    OnEmote?.Invoke(emote);
                    break;
                case nameof(GameStartEvent):
                    var gameStart = message.GetAs<GameStartEvent>();
                    OnGameStart?.Invoke(gameStart);
                    break;
                case nameof(ExceptionEvent):
                    var exception = message.GetAs<ExceptionEvent>();
                    OnException?.Invoke(exception);
                    break;
                case nameof(GameEndEvent):
                    var gameEnd = message.GetAs<GameEndEvent>();
                    OnGameEnd?.Invoke(gameEnd);
                    return;
                case nameof(YourSideEvent):
                    var yourSide = message.GetAs<YourSideEvent>();
                    OnYouSide?.Invoke(yourSide);
                    break;
            }
        }
    }

    public void Dispose() => _connection.Dispose();

    public event TurnEventHandler? OnTurn;
    public Task Send<T>(T obj) where T : IGameAction => _connection.Transmit(obj);


    public async Task<InteroperableModel> Play()
    {
        await _connection.Transmit(_gameRequest);
        var _ = await _connection.ReceiveObject<GameAcknowledgeEvent>();
        return new ClientGameModel(this);
    }

    public async Task Connect()
    {
        await _connection.Transmit(_connectionRequest);
        var _ = await _connection.ReceiveObject<ConnectAcknowledgeEvent>();
    }

    public async Task Disconnect()
    {
        await _connection.Transmit(_disconnectRequest);
        await _connection.ReceiveObject<DisconnectAcknowledgeEvent>();
    }

    public event MoveEventHandler? OnMove;
    public event EmoteEventHandler? OnEmote;
    public event GameStartEventHandler? OnGameStart;
    public event ExceptionEventHandler? OnException;
    public event GameEndEventHandler? OnGameEnd;
    public event YourSideEventHandler? OnYouSide;
}


