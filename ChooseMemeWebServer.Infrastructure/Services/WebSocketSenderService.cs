using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core.Entities;
using ChooseMemeWebServer.Infrastructure.Extensions;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class WebSocketSenderService : IWebSocketSenderService
    {
        private readonly IWebSocketConnectionService _connectionManager;

        public WebSocketSenderService(IWebSocketConnectionService connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task SendMessageBroadcast(Lobby lobby, WebSocketResponseMessage payload)
        {
            await _connectionManager.SendMessageBroadcast(lobby, payload);
        }

        public async Task SendMessageBroadcastWithoutServer(Lobby lobby, WebSocketResponseMessage payload)
        {
            await _connectionManager.SendMessageBroadcastWithoutServer(lobby, payload);
        }

        public async Task SendMessageToPlayer(Player player, WebSocketResponseMessage payload)
        {
            await _connectionManager.SendMessageToPlayer(player, payload);
        }

        public async Task SendMessageToServer(Lobby lobby, WebSocketResponseMessage payload)
        {
            await _connectionManager.SendMessageToServer(lobby, payload);
        }
    }
}
