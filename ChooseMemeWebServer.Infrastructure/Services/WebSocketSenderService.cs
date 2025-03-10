using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions;
using ChooseMemeWebServer.Infrastructure.Extensions;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class WebSocketSenderService : IWebSocketSenderService
    {
        private readonly IWebSocketConnectionService connectionService;

        public WebSocketSenderService(IWebSocketConnectionService connectionManager)
        {
            connectionService = connectionManager;
        }

        public async Task SendMessageBroadcast(Lobby lobby, WebSocketResponseMessage payload)
        {
            await SendMessageToServer(lobby.Server, payload);
            await SendMessageToAllPlayers(lobby, payload);
        }

        public async Task SendMessageToAllPlayers(Lobby lobby, WebSocketResponseMessage payload)
        {
            foreach (var player in lobby.Players.Where(p => !p.IsBot).ToList())
            {
                await SendMessageToPlayer(player, payload);
            }
        }

        public async Task SendMessageToPlayer(Player player, WebSocketResponseMessage payload)
        {
            try
            {
                if (player == null)
                {
                    throw new CannotGetPlayerConnectionException("Unknown");
                }

                if (player.IsBot)
                {
                    return;
                }

                if (!connectionService.TryGetPlayerConnection(player, out var playerWebSocket))
                {
                    throw new CannotGetPlayerConnectionException(player.Username);
                }

                await playerWebSocket.WriteDataToWebSocket(payload);
            }
            catch (CannotGetPlayerConnectionException ex)
            {
                return;
            }
            catch (WebSocketException ex)
            {
                return;
            }
        }

        public async Task SendMessageToServer(Server server, WebSocketResponseMessage payload)
        {
            try
            {
                if (server == null || !connectionService.TryGetServerConnection(server, out var serverWebSocket))
                {
                    throw new CannotGetServerConnectionException();
                }

                await serverWebSocket.WriteDataToWebSocket(payload);
            }
            catch (CannotGetServerConnectionException ex)
            {
                return;
            }
            catch (WebSocketException ex)
            {
                return;
            }
        }

        public async Task SendMessageToServer(Lobby lobby, WebSocketResponseMessage payload)
        {
            await SendMessageToServer(lobby.Server, payload);
        }
    }
}
