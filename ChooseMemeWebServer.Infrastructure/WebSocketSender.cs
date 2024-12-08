using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Infrastructure
{
    public class WebSocketSender : IWebSocketSender
    {
        private readonly WebSocketConnectionManager _connectionManager;

        public WebSocketSender(WebSocketConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task SendMessageBroadcast(Lobby lobby, WebSocketData payload)
        {
            await _connectionManager.SendMessageBroadcast(lobby, payload);
        }

        public async Task SendMessageBroadcastWithoutServer(Lobby lobby, WebSocketData payload)
        {
            await _connectionManager.SendMessageBroadcastWithoutServer(lobby, payload);
        }

        public async Task SendMessageToPlayer(Player player, WebSocketData payload)
        {
            await _connectionManager.SendMessageToPlayer(player, payload);
        }

        public async Task SendMessageToServer(Lobby lobby, WebSocketData payload)
        {
            await _connectionManager.SendMessageToServer(lobby, payload);
        }
    }
}
