using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ChooseMemeWebServer.Infrastructure.Extensions
{
    public static class WebSocketExtentions
    {
        public static async Task WriteDataToWebSocket(this WebSocket webSocket, WebSocketResponseMessage data)
        {
            if(webSocket.State == WebSocketState.Closed)
            {
                throw new WebSocketException();
            }

            string json = JsonSerializer.Serialize(data);

            var buffer = Encoding.UTF8.GetBytes(json);

            await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public static async Task<BetterWebSocketReceiveResult> ReadDataFromWebSocket(this WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

            BetterWebSocketReceiveResult result = new BetterWebSocketReceiveResult(receiveResult.Count,
                (BetterWebSocketMessageType)receiveResult.MessageType, receiveResult.EndOfMessage, (BetterWebSocketCloseStatus?)receiveResult.CloseStatus,
            receiveResult.CloseStatusDescription, buffer[..receiveResult.Count]);

            return result;
        }

        public static async Task SendMessageBroadcast(this IWebSocketConnectionService connectionService, Lobby lobby, WebSocketResponseMessage payload)
        {
            await connectionService.SendMessageToServer(lobby.Server, payload);
            await connectionService.SendMessageToAllPlayers(lobby, payload);
        }

        public static async Task SendMessageToAllPlayers(this IWebSocketConnectionService connectionService, Lobby lobby, WebSocketResponseMessage payload)
        {
            foreach (var player in lobby.Players)
            {
                await connectionService.SendMessageToPlayer(player, payload);
            }
        }

        public static async Task SendMessageToPlayer(this IWebSocketConnectionService connectionService, Player player, WebSocketResponseMessage payload)
        {
            try
            {
                if (!connectionService.TryGetPlayerConnection(player, out var playerWebSocket))
                {
                    throw new CannotGetPlayerConnectionException(player.Username);
                }

                await playerWebSocket.WriteDataToWebSocket(payload);
            }
            catch (Exception ex)
            {
            }
        }

        public static async Task SendMessageToServer(this IWebSocketConnectionService connectionService, Server server, WebSocketResponseMessage payload)
        {
            if (!connectionService.TryGetServerConnection(server, out var serverWebSocket))
            {
                throw new CannotGetServerConnectionException();
            }

            await serverWebSocket.WriteDataToWebSocket(payload);
        }
    }
}
