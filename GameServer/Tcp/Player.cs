using Common.Entity;
using GameModel;
using GameServer.GameRepository;
using GameTransmission;
using static Common.CommunicationProtocol;
using static GameServer.IPlayer;
using static GameTransmission.Message;
using SurrenderAction = GameModel.SurrenderAction;

namespace GameServer.Tcp;

public sealed class Player : IPlayer, IDisposable
{
    private readonly Connection _connection;
    private readonly IPlayerRepository _repository;
    private readonly Credential _credential;


    internal Player(IPlayerRepository repository, Connection connection, Credential credential)
    {
        _connection = connection;
        _repository = repository;
        _credential = credential;
    }

    internal async void Listen()
    {
        while (true)
        {
            var json = await _connection.ReceiveString();
            if (json == null) continue;
            var message = FromString(json);
            if (message == null) continue;
            switch (message.Type)
            {
                case nameof(GameRequestAction):
                    OnGameRequest?.Invoke(this);
                    break;
                case nameof(DisconnectRequestAction):
                    OnDisconnect?.Invoke(this);
                    break;
                case nameof(MoveAction):
                    {
                        var move = message.GetAs<MoveAction>();
                        OnMove?.Invoke(move);
                        break;
                    }
                case nameof(SurrenderAction):
                    {
                        var s = Deserialize<SurrenderAction>(json);
                        if (s == null) break;
                        OnSurrender?.Invoke(s);
                        break;
                    }
                case nameof(EmoteAction):
                    {
                        var emote = message.GetAs<EmoteAction>();
                        OnEmote?.Invoke(emote);
                        break;
                    }
            }
        }
    }

    public void Dispose() => _connection.Dispose();

    public PlayerInfo PlayerData => _repository.GetInfo(_credential);
    public event MoveActionHandler? OnMove;
    public event EmoteActionHandler? OnEmote;
    public event SurrenderHandler? OnSurrender;
    public event GameRequestHandler? OnGameRequest;
    public event DisconnectActionHandler? OnDisconnect;

    public void Send<T>(T obj) => _connection.Transmit(obj).Wait();
}
