using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Services
{
    public class ServerService : IServerService
    {
        public Dictionary<string, Server> onlineServers = new Dictionary<string, Server>();

        public Server AddOnlineServer()
        {
            Server server = new Server()
            {
                Id = Guid.NewGuid().ToString()
            };

            onlineServers.Add(server.Id, server);

            return server;
        }

        public void RemoveOnlineServer(Server server)
        {
            onlineServers.Remove(server.Id);
        }

        public void GetOnlineServers()
        {

        }

        public Server? GetOnlineServer(string serverId)
        {
            Server? resultServer = new Server();

            if (onlineServers.TryGetValue(serverId, out Server? server))
            {
                resultServer = server;
            }

            return resultServer;
        }
    }
}
