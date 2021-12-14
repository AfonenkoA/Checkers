using GameModel;

namespace GameClient;

public interface IClient
{
    public Task<InteroperableModel> Play();
    public Task Connect();
    public Task Disconnect();
}