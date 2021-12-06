using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Checkers.Data.Old;
using Checkers.Game.Old;
using static System.Text.Json.JsonSerializer;
using EventArgs = Checkers.Game.Old.EventArgs;
using Game = Checkers.Data.Old.Game;
using User = Checkers.Data.Old.User;

namespace OldGameServer;

internal static class Server
{
    private static void Main(string[] args)
    {
        const int port = 5000;
        using var db = new GameDatabase.Factory(args[0]).Get();
        TcpServer server = new(port, db);
        Task.Run(server.Run);
    }
}

class PlayerFactory
{
    public static Player Connect(TcpClient client, GameDatabase database)
    {
        return new Player(client, database);
    }
}

internal sealed class Player : IDisposable
{
    public event EventHandler<MoveActionArgs> OnMove = (_, _) => { };
    public event EventHandler<EmoteActionArgs> OnEmote = (_, _) => { };
    public event EventHandler<SurrenderActionArgs> OnSurrender = (_, _) => { };
    public event EventHandler<RequestForGameActionArgs> OnRequest = (_, _) => { };
    public event EventHandler<DisconnectActionArgs> OnDisconnect = (_, _) => { };

    private readonly TcpClient client;
    private readonly StreamReader reader;
    private readonly StreamWriter writer;
    private readonly GameDatabase database;
    private User user;


    public string Nick => user.Login;
    public int Id => user.Id;
    public int AnimationsId => user.SelectedAnimation;
    public int CheckersId => user.SelectedCheckers;

    public Player(TcpClient client, GameDatabase database)
    {
        this.client = client;
        this.database = database;
        reader = new(client.GetStream());
        writer = new(client.GetStream()) { AutoFlush = true };
        Task.Run(Listen);
    }


    public async Task SendEvent<T>(T e) where T : EventArgs
    {
        await writer.WriteLineAsync(Serialize(e));
    }

    private async void Listen()
    {
        while (true)
        {
            string msg = await reader.ReadLineAsync();
            switch (Deserialize<ActionArgs>(msg).Type)
            {
                case ActionType.Connect:
                    ConnectAction connect = Deserialize<ConnectAction>(msg);
                    user = database.FindUser(connect.Login, connect.Password);
                    await writer.WriteLineAsync(Serialize(ConnectionAcceptEventArgs.Instance));
                    break;
                case ActionType.Move:
                    OnMove(this, Deserialize<MoveActionArgs>(msg));
                    break;
                case ActionType.Emote:
                    OnEmote(this, Deserialize<EmoteActionArgs>(msg));
                    break;
                case ActionType.Surrender:
                    OnSurrender(this, Deserialize<SurrenderActionArgs>(msg));
                    break;
                case ActionType.RequestForGame:
                    OnRequest(this, Deserialize<RequestForGameActionArgs>(msg));
                    break;
                case ActionType.Disconnect:
                    OnDisconnect(this, Deserialize<DisconnectActionArgs>(msg));
                    return;
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
class TcpServer
{
    private readonly GameDatabase database;
    private readonly IPAddress ipAddress;
    private readonly int port;
    private readonly List<Player> list = new();
    private readonly Queue<Player> gameQueue = new();
    public TcpServer(int port, GameDatabase database)
    {
        ipAddress = IPAddress.Loopback;
        this.port = port;
        this.database = database;
    }
    public async void Run()
    {
        TcpListener listener = new(ipAddress, port);
        listener.Start();
        Console.WriteLine($"Запущен сервер на {ipAddress}:{port}");
        while (true)
        {
            Player p = new(await listener.AcceptTcpClientAsync(), database);
            Console.WriteLine($"Подключен пользователь {p}");
            list.Add(p);
            p.OnRequest += (_, _) =>
            {
                gameQueue.Enqueue(p);
                if (gameQueue.Count >= 2)
                    new GameHolder(gameQueue.Dequeue(), gameQueue.Dequeue(), database).Start();
            };
        }
    }

    public class GameHolder
    {
        private readonly Player p1;
        private readonly Player p2;
        private readonly GameDatabase database;
        private int turnNumber = 1;

        private readonly Game game;

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

        public GameHolder(Player p1, Player p2, GameDatabase database)
        {
            this.database = database;
            this.p1 = p1;
            this.p2 = p2;
            game = new Game
            {
                Player1Id = p1.Id,
                Player2Id = p2.Id,
                Player1AnimationId = p1.AnimationsId,
                Player1ChekersId = p1.CheckersId,
                Player2AnimationId = p2.AnimationsId,
                Player2ChekersId = p2.CheckersId,
                StartTime = DateTime.Now,
            };
        }


        //public DateTime StartTime { get; set; }
        //public DateTime EndTime { get; set; }
        //public int WinnerId { get; set; }
        //public int Player1RaitingChange { get; set; }
        //public int Player2RaitingChange { get; set; }

        public void Start()
        {
            Task p1Start = p1.SendEvent(new GameStartEventArgs { EnemyNick = p1.Nick });
            Task p2Start = p2.SendEvent(new GameStartEventArgs { EnemyNick = p2.Nick });
            Task.WaitAll(p1Start, p2Start);
            Task p1Turn = p1.SendEvent(YourTurnEventArgs.Instance);
            Task p2Turn = p2.SendEvent(EnemyTurnEventArgs.Instance);
            Task.WaitAll(p1Turn, p2Turn);
            Subscribe();
        }
        private void MoveHolder(object o, MoveActionArgs action)
        {
            Player p = o as Player;
            Console.WriteLine(action);
            Task p1Turn, p2Turn, p1Move, p2Move;
            p1Move = p1.SendEvent(new MoveEventArgs(action));
            p2Move = p2.SendEvent(new MoveEventArgs(action));
            if (ReferenceEquals(p1, p))
            {
                p1Turn = p1.SendEvent(EnemyTurnEventArgs.Instance);
                p2Turn = p2.SendEvent(YourTurnEventArgs.Instance);
            }
            else
            {
                p1Turn = p1.SendEvent(YourTurnEventArgs.Instance);
                p2Turn = p2.SendEvent(EnemyTurnEventArgs.Instance);
            }
            Task.WaitAll(p1Turn, p2Turn, p1Move, p2Move);
        }
        private void EmoteHolder(object o, EmoteActionArgs action)
        {
            GamesProgress progress = new()
            {
                ActionNum = turnNumber++,
            };
        }
        private void SurrenderHolder(object o, SurrenderActionArgs action)
        {
            Player p = o as Player;
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
            Task p1End = p1.SendEvent(new GameEndEventArgs { Result = p1Result });
            Task p2End = p2.SendEvent(new GameEndEventArgs { Result = p2Result });
            Task.WaitAll(p1End, p2End);
            Unsubscribe();
        }
    }
}