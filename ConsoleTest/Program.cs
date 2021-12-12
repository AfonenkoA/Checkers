using System.Net;
using System.Net.Sockets;
using Common.Entity;
using GameClient;
using GameModel;
using GameTransmission;
using static System.Console;
using static System.Threading.Tasks.Task;
using static GameModel.Side;

var tcp = new TcpClient();
await tcp.ConnectAsync(IPAddress.Loopback, Connection.ServerPort);
var c = new Client(new Connection(tcp), new Credential { Login = "log", Password = "pass" });
var _ = Run(c.Listen);
c.OnConnectAcknowledge += _ => WriteLine("OnConnectAcknowledge");
c.OnDisconnectAcknowledge += _ => WriteLine("OnDisconnectAcknowledge");
c.OnEmote += _ => WriteLine("OnEmote");
c.OnException += _ => WriteLine("OnException");
c.OnGameEnd += _ => WriteLine("GameEnd");
c.OnGameStart += _ => WriteLine("OnGameStart");
c.OnKill += _ => WriteLine("OnKill");
c.OnYouSide += _ => WriteLine("OnYourSide");
c.OnMove += _ => WriteLine("OnMove");

async Task Act()
{
    WriteLine("1:   ConnectionRequest");
    WriteLine("2:   DisconnectRequest");
    WriteLine("3:   MoveAction");
    WriteLine("4:   EmoteAction");
    WriteLine("5:   SurrenderAction");
    WriteLine("6:   GameRequest");
    while (true)
    {
        switch (Read())
        {
            case '1':
                await c.Connect();
                break;
            case '2':
                await c.Disconnect();
                break;
            case '3':
                await c.Send(new MoveAction { Side = White, From = 0, To = 5 });
                break;
            case '4':
                await c.Send(new EmoteAction { Side = White, Id = 3 });
                break;
            case '5':
                await c.Send(new SurrenderAction { Side = White });
                break;
            case '6':
                await c.GameRequest();
                break;
        }
    }
}

Run(Act).Wait();