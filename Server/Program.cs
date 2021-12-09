
using GameServer.Tcp;

var server = TcpServer.CreateServer(int.Parse(args[0]));
server.Run().Wait();