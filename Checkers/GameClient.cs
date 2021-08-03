using System;
using System.Collections.Generic;
using Checkers.Transmission.InGame;
using Checkers.Transmission;
using System.Net.Sockets;
using System.IO;
using System.Net;
using static System.Text.Json.JsonSerializer;
using System.Threading.Tasks;
using System.Net.Http;

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

    public struct Position
    {
        private const string letters = "ABCDEFGH";
        public readonly int x, y;
        public Position(int x, int y)
        {
            if (x < 8)
                this.x = x;
            else throw new ArgumentOutOfRangeException($"x = {x}");
            if (y < 8)
                this.y = y;
            else throw new ArgumentOutOfRangeException($"y = {y}");
        }
        public Position(string state)
        {
            x = state[0] - 'A';
            y = state[1] - '0';
        }
        public override string ToString()
        {
            return letters[x].ToString() + y.ToString();
        }

    }

    public struct Checker
    {
        public readonly CheckerType type;
        public readonly Position position;
    }

    public class GameClient
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
        public string Login { get { return _login; } }
        public string Password { get { return _password; } }

        private const string _baseUri = "http://localhost:5005/api/";
        private const string _userUri = _baseUri + "user/";
        private const string _gameUri = _baseUri + "games/";

        private string AchievementsUri
        {
            get
            {
                return _userUri + Login + "/achievements";
            }
        }

        private string ItemsUri
        {
            get
            {
                return _userUri + Login + "/items";
            }
        }

        private string FriendsUri
        {
            get
            {
                return _userUri + Login + "/friends";
            }
        }

        private string UserGamesUri
        {
            get
            {
                return _userUri + Login + "/games";
            }
        }

        public class GameService : IDisposable
        {
            public static class Board
            {
                public static readonly CellType[,] schema = new CellType[8, 8];
                public static readonly IReadOnlyList<Checker> Checkers;
                static Board()
                {
                    bool ind = false;
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (ind = !ind)
                                schema[i, j] = CellType.Black;
                            else
                                schema[i, j] = CellType.White;
                        }
                    List<Checker> list = new();

                    Checkers = list;
                }
            }
            public class GameController
            {
                private StreamWriter _writer;
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
                    Send(Serialize(RequestForGameAction.Instance));
                }

                public void Move(Position from, Position to)
                {
                    Send(Serialize(new MoveAction() { From = from, To = to }));
                }

                public void Emote(int emotionID)
                {
                    Send(Serialize(new EmoteAction() { EmotionID = emotionID }));
                }

                public void Surrender()
                {
                    Send(Serialize(SurrenderAction.Instance));
                }
            }

            public delegate void GameStartEventHandler(GameStartEvent? ev);
            public delegate void GameEndEventHandler(GameEndEvent? ev);
            public delegate void YourTurnEventHandler(YourTurnEvent? ev);
            public delegate void EnemyTurnEventHandler(EnemyTurnEvent? ev);
            public delegate void MoveEventHandelr(MoveEvent? ev);
            public delegate void EmoteEventHandler(EmoteEvent? ev);
            public delegate void RemoveEventHandler(RemoveEvent? ev);
            public delegate void ConnectionAcceptEventHandler(ConnectionAcceptEvent? ev);
            public delegate void DisconnectEventHandler(DisconnectEvent? ev);

            public event GameStartEventHandler OnGameStart = Console.WriteLine;
            public event GameEndEventHandler OnGameEnd = Console.WriteLine;
            public event YourTurnEventHandler OnYourTurn = Console.WriteLine;
            public event EnemyTurnEventHandler OnEnemyTurn = Console.WriteLine;
            public event MoveEventHandelr OnMove = Console.WriteLine;
            public event RemoveEventHandler OnRemove = Console.WriteLine;
            public event EmoteEventHandler OnEmote = Console.WriteLine;
            public event ConnectionAcceptEventHandler OnConnectionAccept = Console.WriteLine;
            public event DisconnectEventHandler OnDisconnect = Console.WriteLine;

            private readonly TcpClient tcp;
            private StreamWriter? _writer;
            private readonly GameClient _client;

            public GameService(GameClient client)
            {
                tcp = new();
                _client = client;
            }

            public void Connect()
            {
                tcp.Connect(IPAddress.Loopback, 5000);
                _writer = new(tcp.GetStream()) { AutoFlush = true };
                Controller = new GameController(_writer);
                _writer.WriteLine(Serialize(new ConnectAction() { Login = _client.Login, Password = _client.Password }));
                Task.Run(Listen);
            }

            public void Disconnect()
            {
                _writer?.WriteLine(Serialize(DisconnectAction.Instance));
            }

            public GameController? Controller { get; private set; }

            private async void Listen()
            {
                StreamReader reader = new(tcp.GetStream());
                while (true)
                {
                    string message = await reader.ReadLineAsync();
                    switch (Deserialize<Event>(message).Type)
                    {
                        case EventType.GameStart:
                            OnGameStart(Deserialize<GameStartEvent>(message));
                            break;
                        case EventType.GameEnd:
                            OnGameEnd(Deserialize<GameEndEvent>(message));
                            break;
                        case EventType.YourTurn:
                            OnYourTurn(Deserialize<YourTurnEvent>(message));
                            break;
                        case EventType.EnemyTurn:
                            OnEnemyTurn(Deserialize<EnemyTurnEvent>(message));
                            break;
                        case EventType.Emote:
                            OnEmote(Deserialize<EmoteEvent>(message));
                            break;
                        case EventType.Move:
                            OnMove(Deserialize<MoveEvent>(message));
                            break;
                        case EventType.Remove:
                            OnRemove(Deserialize<RemoveEvent>(message));
                            break;
                        case EventType.ConnectionAccept:
                            OnConnectionAccept(Deserialize<ConnectionAcceptEvent>(message));
                            break;
                        case EventType.Disconnect:
                            OnDisconnect(Deserialize<DisconnectEvent>(message));
                            return;
                    }
                }
            }

            public void Dispose()
            {
                tcp.Close();
                tcp.Dispose();
            }
        }

        public async Task<UserAuthorizationResponse> AuthorizeAsync() => Deserialize<UserAuthorizationResponse>(
                await _httpClient.GetStringAsync(_userUri + _query+"action=authorize"));

        public async Task<UserInfoResponse> GetUserInfoAsync() => Deserialize<UserInfoResponse>(
            await _httpClient.GetStringAsync(_userUri + _query + "action=info"));

        public async Task<UserAchievementsGetResponse> GetAchievementsAsync() =>
            Deserialize<UserAchievementsGetResponse>(await _httpClient.GetStringAsync(AchievementsUri));

        public async Task<UserFriendsResponse> GetFriendsAsync() =>
            Deserialize<UserFriendsResponse>(await _httpClient.GetStringAsync(FriendsUri + _query));

        public async Task<UserGamesGetResponse> GetGamesAsync() =>
            Deserialize<UserGamesGetResponse>(await _httpClient.GetStringAsync(UserGamesUri));

        public async Task<GameGetRespose> GetGameAsync(int id) =>
            Deserialize<GameGetRespose>(await _httpClient.GetStringAsync(_gameUri + id));

        public async Task<UserItemsResponse> GetItemsAsync() =>
            Deserialize<UserItemsResponse>(await _httpClient.GetStringAsync(ItemsUri + _query));

        public async Task<UserGetResponse> GetUserAsync(string login) => 
            Deserialize<UserGetResponse>(await _httpClient.GetStringAsync(_userUri+login));
    }
}