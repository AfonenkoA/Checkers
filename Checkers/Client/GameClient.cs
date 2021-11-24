using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Checkers.Transmission;
using InGame;

namespace Checkers.Client
{
    public enum CellType
    {
        Black,
        White
    }

    public enum CheckerType
    {
        Black,
        White
    }

    public readonly struct Position
    {
        private const string Letters = "ABCDEFGH";
        private readonly int _x;
        private readonly int _y;

        public Position(int x, int y)
        {
            if (x < 8)
                _x = x;
            else throw new ArgumentOutOfRangeException($"x = {x}");
            if (y < 8)
                _y = y;
            else throw new ArgumentOutOfRangeException($"y = {y}");
        }
        public Position(string state)
        {
            _x = state[0] - 'A';
            _y = state[1] - '0';
        }
        public override string ToString()
        {
            return Letters[_x] + _y.ToString();
        }

    }

    public struct Checker
    {
        public readonly CheckerType type;
        public readonly Position position;
    }

    public sealed class GameClient
    {
        private readonly string _login;
        private readonly string _password;
        private GameService? _gameService;

        private readonly HttpClient _httpClient = new();

        private readonly string _query;

        public GameClient(string login, string password)
        {
            _login = login;
            _password = password;
            _query = $"?login={login}&password={password}&";
        }

        public GameService Service
        {
            get
            {
                if (_gameService == null)
                    _gameService = new GameService(this);
                return _gameService;
            }
        }

        private string Login { get { return _login; } }
        public string Password { get { return _password; } }

        private const string BaseUri = "http://localhost:5005/api/";
        private const string UserUri = BaseUri + "user/";
        private const string GameUri = BaseUri + "games/";

        private string AchievementsUri => UserUri + Login + "/achievements";

        private string ItemsUri => UserUri + Login + "/items";

        private string FriendsUri => UserUri + Login + "/friends";

        private string UserGamesUri => UserUri + Login + "/games";

        public class GameService : IDisposable
        {
            public static class Board
            {
                private static readonly CellType[,] Schema = new CellType[8, 8];
                private static readonly IReadOnlyList<Checker> Checkers;
                static Board()
                {
                    var ind = false;
                    for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        if (ind == !ind)
                            Schema[i, j] = CellType.Black;
                        else
                            Schema[i, j] = CellType.White;
                    }
                    List<Checker> list = new();

                    Checkers = list;
                }
            }
            public sealed class GameController
            {
                private readonly StreamWriter _writer;
                internal GameController(StreamWriter writer)
                {
                    _writer = writer;
                }
                private void Send(string str)
                {
                    _writer.WriteLine(str);
                }

                public void Request()
                {
                    Send(JsonSerializer.Serialize(RequestForGameActionArgs.Instance));
                }

                public void Move(Position from, Position to)
                {
                    Send(JsonSerializer.Serialize(new MoveActionArgs() { From = from, To = to }));
                }

                public void Emote(int emotionId)
                {
                    Send(JsonSerializer.Serialize(new EmoteActionArgs() { EmotionID = emotionId }));
                }

                public void Surrender()
                {
                    Send(JsonSerializer.Serialize(SurrenderActionArgs.Instance));
                }
            }

            private static void Print(object? sender, object args) => Console.WriteLine(args);

            public event EventHandler<GameStartEventArgs> OnGameStart = Print;
            public event EventHandler<GameEndEventArgs> OnGameEnd = Print;
            public event EventHandler<YourTurnEventArgs> OnYourTurn = Print;
            public event EventHandler<EnemyTurnEventArgs> OnEnemyTurn = Print;
            public event EventHandler<MoveEventArgs> OnMove = Print;
            public event EventHandler<RemoveEventArgs> OnRemove = Print;
            public event EventHandler<EmoteEventArgs> OnEmote = Print;
            public event EventHandler<ConnectionAcceptEventArgs> OnConnectionAccept = Print;
            public event EventHandler<DisconnectEventArgs> OnDisconnect = Print;

            private readonly TcpClient tcp;
            private StreamWriter? _writer;
            private readonly GameClient _client;

            internal GameService(GameClient client)
            {
                tcp = new TcpClient();
                _client = client;
            }

            public void Connect()
            {
                tcp.Connect(IPAddress.Loopback, 5000);
                _writer = new(tcp.GetStream()) { AutoFlush = true };
                Controller = new GameController(_writer);
                _writer.WriteLine(JsonSerializer.Serialize(new ConnectAction() { Login = _client.Login, Password = _client.Password }));
                Task.Run(Listen);
            }

            public void Disconnect()
            {
                _writer?.WriteLine(JsonSerializer.Serialize(DisconnectActionArgs.Instance));
            }

            public GameController? Controller { get; private set; }

            private async void Listen()
            {
                StreamReader reader = new(tcp.GetStream());
                while (true)
                {
                    string? message = await reader.ReadLineAsync();
                    if (message == null)
                        continue;
                    var type = JsonSerializer.Deserialize<InGame.EventArgs>(message)?.Type;
                    if (type == null)
                        continue;
                    switch (type)
                    {
                        case EventType.GameStart:
                            OnGameStart(this, JsonSerializer.Deserialize<GameStartEventArgs>(message) ?? new GameStartEventArgs());
                            break;
                        case EventType.GameEnd:
                            OnGameEnd(this, JsonSerializer.Deserialize<GameEndEventArgs>(message) ?? new GameEndEventArgs());
                            break;
                        case EventType.YourTurn:
                            OnYourTurn(this, JsonSerializer.Deserialize<YourTurnEventArgs>(message) ?? YourTurnEventArgs.Instance);
                            break;
                        case EventType.EnemyTurn:
                            OnEnemyTurn(this, JsonSerializer.Deserialize<EnemyTurnEventArgs>(message) ?? EnemyTurnEventArgs.Instance);
                            break;
                        case EventType.Emote:
                            OnEmote(this, JsonSerializer.Deserialize<EmoteEventArgs>(message) ?? new EmoteEventArgs());
                            break;
                        case EventType.Move:
                            OnMove(this, JsonSerializer.Deserialize<MoveEventArgs>(message) ?? new MoveEventArgs());
                            break;
                        case EventType.Remove:
                            OnRemove(this, JsonSerializer.Deserialize<RemoveEventArgs>(message) ?? new RemoveEventArgs());
                            break;
                        case EventType.ConnectionAccept:
                            OnConnectionAccept(this, JsonSerializer.Deserialize<ConnectionAcceptEventArgs>(message) ?? ConnectionAcceptEventArgs.Instance);
                            break;
                        case EventType.Disconnect:
                            OnDisconnect(this, JsonSerializer.Deserialize<DisconnectEventArgs>(message) ?? DisconnectEventArgs.Instance);
                            return;
                        case null:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            public void Dispose()
            {
                tcp.Close();
                tcp.Dispose();
            }
        }

        public async Task<UserAuthorizationResponse> AuthorizeAsync() => JsonSerializer.Deserialize<UserAuthorizationResponse>(
                                                                             await _httpClient.GetStringAsync(UserUri + _query + "action=authorize")) ??
                                                                         UserAuthorizationResponse.Failed;

        public async Task<UserInfoResponse> GetUserInfoAsync() => JsonSerializer.Deserialize<UserInfoResponse>(
                                                                      await _httpClient.GetStringAsync(UserUri + _query + "action=info")) ??
                                                                  UserInfoResponse.Failed;
    

        public async Task<UserAchievementsGetResponse> GetAchievementsAsync() =>
            JsonSerializer.Deserialize<UserAchievementsGetResponse>(await _httpClient.GetStringAsync(AchievementsUri)) ??
            UserAchievementsGetResponse.Failed;

        public async Task<UserFriendsResponse> GetFriendsAsync() =>
            JsonSerializer.Deserialize<UserFriendsResponse>(await _httpClient.GetStringAsync(FriendsUri + _query)) ??
            UserFriendsResponse.Failed;

        public async Task<UserGamesGetResponse> GetGamesAsync() =>
            JsonSerializer.Deserialize<UserGamesGetResponse>(await _httpClient.GetStringAsync(UserGamesUri)) ??
            UserGamesGetResponse.Failed;

        public async Task<GameGetRespose> GetGameAsync(int id) =>
            JsonSerializer.Deserialize<GameGetRespose>(await _httpClient.GetStringAsync(GameUri + id)) ??
            GameGetRespose.Failed;

        public async Task<UserItemsResponse> GetItemsAsync() =>
            JsonSerializer.Deserialize<UserItemsResponse>(await _httpClient.GetStringAsync(ItemsUri + _query)) ??
            UserItemsResponse.Failed;

        public async Task<UserGetResponse> GetUserAsync(string login) =>
            JsonSerializer.Deserialize<UserGetResponse>(await _httpClient.GetStringAsync(UserUri + login)) ?? 
            UserGetResponse.Failed;
    }
}
