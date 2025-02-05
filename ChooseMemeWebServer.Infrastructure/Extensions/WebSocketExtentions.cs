using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core.Entities;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ChooseMemeWebServer.Infrastructure.Extensions
{
    public static class WebSocketExtentions
    {
        public static async Task WriteDataToWebSocket(this WebSocket webSocket, WebSocketResponseMessage data)
        {
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
            await connectionService.SendMessageToServer(lobby, payload);
            await connectionService.SendMessageBroadcastWithoutServer(lobby, payload);
        }

        public static async Task SendMessageBroadcastWithoutServer(this IWebSocketConnectionService connectionService, Lobby lobby, WebSocketResponseMessage payload)
        {
            foreach (var player in lobby.Players)
            {
                await connectionService.SendMessageToPlayer(player, payload);
            }
        }

        public static async Task SendMessageToPlayer(this IWebSocketConnectionService connectionService, Player player, WebSocketResponseMessage payload)
        {
            if (connectionService.TryGetPlayerConnection(player, out var playerWebSocket))
            {
                await playerWebSocket.WriteDataToWebSocket(payload);
            }
        }

        public static async Task SendMessageToServer(this IWebSocketConnectionService connectionService, Lobby lobby, WebSocketResponseMessage payload)
        {
            if (connectionService.TryGetServerConnection(lobby, out var serverWebSocket))
            {
                await serverWebSocket.WriteDataToWebSocket(payload);
            }
        }
    }
}
