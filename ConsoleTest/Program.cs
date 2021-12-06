using Checkers.Game.Client;
using Checkers.Game.Server;
using Checkers.Game.Server.Transmission;
using static System.Console;


static void PlayerEvent(object? _,EventArgs eventArgs) =>
    WriteLine("Payer");

static void ServerStart(object? o,EventArgs e) => WriteLine($"Server start");

static void ClientAccept(object? _,ConnectionAcceptEvent? e) =>
    WriteLine("Client");

const int port = 3006;
var server = new Server(port);
server.OnStart += ServerStart;
server.OnPlayerConnected += PlayerEvent;
Task.Run(server.Run);
var client = new Client(port);
client.OnConnect += ClientAccept;
Task.Run(client.Listen);
while (true)
{
    Read();
    client.Send();
}