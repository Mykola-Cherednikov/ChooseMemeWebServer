using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface IWebSocketSender
    {
        public Task SendMessageToPlayer(Player player, WebSocketData payload);

        public Task SendMessageToServer(Lobby lobby, WebSocketData payload);

        public Task SendMessageBroadcastWithoutServer(Lobby lobby, WebSocketData payload);

        public Task SendMessageBroadcast(Lobby lobby, WebSocketData payload);
    }
}
