using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IWebSocketSenderService
    {
        public Task SendMessageToPlayer(Player player, WebSocketResponseMessage payload);

        public Task SendMessageToServer(Server server, WebSocketResponseMessage payload);

        public Task SendMessageToServer(Lobby lobby, WebSocketResponseMessage payload);

        public Task SendMessageToAllPlayers(Lobby lobby, WebSocketResponseMessage payload);

        public Task SendMessageBroadcast(Lobby lobby, WebSocketResponseMessage payload);
    }
}
