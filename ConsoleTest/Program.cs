using System.Net;
using System.Net.Sockets;
using Common.Entity;
using GameClient;
using GameModel;
using GameTransmission;
using static System.Console;
using static GameModel.Side;

var acting = true;
var tcp = new TcpClient();
await tcp.ConnectAsync(IPAddress.Loopback, Connection.ServerPort);
var c = new Client(new Connection(tcp), new Credential { Login = "log", Password = "pass" });


await c.Connect();
var m = await c.Play();
m.OnEmote += _ => WriteLine("OnEmote");
m.OnException += _ => WriteLine("OnException");
m.OnGameEnd += _ =>
{
    acting = false;
    WriteLine("GameEnd");
};
m.OnGameStart += _ => WriteLine("OnGameStart");
c.OnYouSide += _ => WriteLine("OnYourSide");
m.OnMove += _ => WriteLine("OnMove");
m.OnTurn += _ => WriteLine("Turn");

void Act()
{
    WriteLine("3:   MoveAction");
    WriteLine("4:   EmoteAction");
    WriteLine("5:   SurrenderAction");
    while (acting)
    {
        switch (Read())
        {
            case '3':
                if (!acting) return;
                m.Move(new MoveAction { Side = White, From = 0, To = 5 });
                break;

            case '4':
                if (!acting) return;
                m.Emote(new EmoteAction { Side = White, Emotion = Emotion.Invalid });
                break;
            case '5':
                if (!acting) return;
                m.Surrender(new SurrenderAction { Side = White });
                break;
        }
    }
}
Act();
await c.Disconnect();