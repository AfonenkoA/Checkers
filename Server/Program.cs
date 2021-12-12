using GameServer.Tcp;
using GameTransmission;

var server = TcpServer.CreateServer(Connection.ServerPort);
server.Run().Wait();