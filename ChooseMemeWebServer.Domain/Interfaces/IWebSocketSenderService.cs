using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IWebSocketSenderService
    {
        public Task SendMessageToPlayer(Player player, WebSocketResponseMessage payload);

        public Task SendMessageToServer(Lobby lobby, WebSocketResponseMessage payload);

        public Task SendMessageBroadcastWithoutServer(Lobby lobby, WebSocketResponseMessage payload);

        public Task SendMessageBroadcast(Lobby lobby, WebSocketResponseMessage payload);
    }
}
