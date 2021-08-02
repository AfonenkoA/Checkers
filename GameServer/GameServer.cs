using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Checkers.Transmission.InGame;

using Action = Checkers.Transmission.InGame.Action;

namespace GameServer
{
    class Server
    {
        static void Main(string[] args)
        {
            int port = 5000;
            TCPServer server = new(port);
            server.Run();
            Console.Read();
        }
    }
    class Player : IDisposable
    {
        private string Serialize<T>(T obj) => JsonSerializer.Serialize<T>(obj);
        private T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json);

        public delegate void MoveActionHandler(Player player, MoveAction action);
        public delegate void EmoteActionHandler(Player player, EmoteAction action);
        public delegate void SurrenderActionHandler(Player player, SurrenderAction action);
        public delegate void RequestForGameActionHandler(RequestForGameAction action);

        public event MoveActionHandler OnMove = (p, a) => { };
        public event EmoteActionHandler OnEmote = (p, a) => { };
        public event SurrenderActionHandler OnSurrender = (p, a) => { };
        public event RequestForGameActionHandler OnRequest = a => { };

        private readonly TcpClient client;
        private readonly StreamReader reader;
        private readonly StreamWriter writer;
        public string Login { get; private set; }
        public string Password { get; private set; }
        public Player(TcpClient client)
        {
            this.client = client;
            reader = new(client.GetStream());
            writer = new(client.GetStream()) { AutoFlush = true };
            Task.Run(Listen);
        }


        public async Task SendEvent<T>(T e) where T : Event
        {
            await writer.WriteLineAsync(JsonSerializer.Serialize(e));
        }

        public async Task Listen()
        {
            while (true)
            {
                string msg = await reader.ReadLineAsync();
                switch (Deserialize<Action>(msg).Type)
                {
                    case ActionType.Connect:
                        ConnectAction connect = Deserialize<ConnectAction>(msg);
                        Login = connect.Login;
                        Password = connect.Password;
                        await writer.WriteLineAsync(Serialize(ConnectionAcceptEvent.Instance));
                        break;
                    case ActionType.Move:
                        OnMove(this, Deserialize<MoveAction>(msg));
                        break;
                    case ActionType.Emote:
                        OnEmote(this, Deserialize<EmoteAction>(msg));
                        break;
                    case ActionType.Surrender:
                        OnSurrender(this, Deserialize<SurrenderAction>(msg));
                        break;
                    case ActionType.RequestForGame:
                        OnRequest(Deserialize<RequestForGameAction>(msg));
                        break;
                }
            }
        }

        public override string ToString()
        {
            return $"{client.Client.RemoteEndPoint}";
        }

        public void Dispose()
        {
            client.Close();
        }
    }
    class TCPServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly List<Player> list = new();
        private readonly Queue<Player> gameQueue = new Queue<Player>();
        public TCPServer(int port)
        {
            ipAddress = IPAddress.Loopback;
            this.port = port;
        }
        public async void Run()
        {
            TcpListener listener = new(ipAddress, port);
            listener.Start();
            Console.WriteLine($"Запущен сервер на {ipAddress}:{port}");
            while (true)
            {
                Player p = new(await listener.AcceptTcpClientAsync());
                Console.WriteLine($"Подключен пользователь {p}");
                list.Add(p);
                p.OnRequest += a =>
                {
                    gameQueue.Enqueue(p);
                    if (gameQueue.Count >= 2)
                        Task.Run(new GameHolder(gameQueue.Dequeue(), gameQueue.Dequeue()).Start);
                };
            }
        }

        public class GameHolder
        {
            private enum TurnType
            {
                FirstPlayer,
                SecondPlayer
            }
            private Player p1;
            private Player p2;
            private TurnType turn;
            private void Subscribe()
            {
                p1.OnMove += MoveHolder;
                p2.OnMove += MoveHolder;
                p1.OnEmote += EmoteHolder;
                p2.OnEmote += EmoteHolder;
                p1.OnSurrender += SurrenderHolder;
                p2.OnSurrender += SurrenderHolder;
            }

            private void Unsubscribe()
            {
                p1.OnMove -= MoveHolder;
                p2.OnMove -= MoveHolder;
                p1.OnEmote -= EmoteHolder;
                p2.OnEmote -= EmoteHolder;
                p1.OnSurrender -= SurrenderHolder;
                p2.OnSurrender -= SurrenderHolder;
            }

            public GameHolder(Player p1, Player p2)
            {
                this.p1 = p1;
                this.p2 = p2;
            }

            public void Start()
            {
                turn = TurnType.FirstPlayer;
                Task p1Start = p1.SendEvent(new GameStartEvent() { EnemyNick = p2.Login });
                Task p2Start = p2.SendEvent(new GameStartEvent() { EnemyNick = p1.Login });
                Task.WaitAll(p1Start, p2Start);
                Task p1Turn = p1.SendEvent(YourTurnEvent.Instance);
                Task p2Turn = p2.SendEvent(EnemyTurnEvent.Instance);
                Task.WaitAll(p1Turn, p2Turn);
                Subscribe();

            }
            private void MoveHolder(Player p, MoveAction action)
            {
                Console.WriteLine(action);
                Task p1Turn, p2Turn, p1Move, p2Move;
                p1Move = p1.SendEvent(new MoveEvent(action));
                p2Move = p2.SendEvent(new MoveEvent(action));
                if (ReferenceEquals(p1, p))
                {
                    turn = TurnType.SecondPlayer;
                    p1Turn = p1.SendEvent(YourTurnEvent.Instance);
                    p2Turn = p2.SendEvent(YourTurnEvent.Instance);
                }
                else
                {
                    turn = TurnType.FirstPlayer;
                    p1Turn = p1.SendEvent(YourTurnEvent.Instance);
                    p2Turn = p2.SendEvent(EnemyTurnEvent.Instance);
                }
                Task.WaitAll(p1Turn, p2Turn, p1Move, p2Move);
            }
            private void EmoteHolder(Player p, EmoteAction action)
            {
                Console.WriteLine(p.Login + action);
            }
            private void SurrenderHolder(Player p, SurrenderAction action)
            {
                GameResult p1Result;
                GameResult p2Result;
                if (ReferenceEquals(p1, p))
                {
                    p1Result = GameResult.Lose;
                    p2Result = GameResult.Win;
                }
                else
                {
                    p1Result = GameResult.Win;
                    p2Result = GameResult.Lose;
                }
                Task p1End = p1.SendEvent(new GameEndEvent() { Result = p1Result });
                Task p2End = p2.SendEvent(new GameEndEvent() { Result = p2Result });
                Task.WaitAll(p1End, p2End);
                Unsubscribe();
            }
        }
    }
}
