using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using ChooseMemeWebServer.Core.Exceptions.ServerExceptions;
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

        public async Task SendMessageToAllPlayers(Lobby lobby, WebSocketResponseMessage payload)
        {
            await _connectionManager.SendMessageToAllPlayers(lobby, payload);
        }

        public async Task SendMessageToPlayer(Player player, WebSocketResponseMessage payload)
        {
            await _connectionManager.SendMessageToPlayer(player, payload);
        }

        public async Task SendMessageToServer(Server server, WebSocketResponseMessage payload)
        {
            try
            {

                if (server == null)
                {
                    throw new ServerNotFoundException();
                }
				await _connectionManager.SendMessageToServer(server, payload);
			}
            catch (Exception ex)
            {
				
			}
        }

        public async Task SendMessageToServer(Lobby lobby, WebSocketResponseMessage payload)
        {
            await SendMessageToServer(lobby.Server, payload);
        }
    }
}
