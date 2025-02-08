using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IServerService
    {
        public Server AddOnlineServer();

        public void RemoveOnlineServer(Server server);

        public void GetOnlineServers();
    }
}
