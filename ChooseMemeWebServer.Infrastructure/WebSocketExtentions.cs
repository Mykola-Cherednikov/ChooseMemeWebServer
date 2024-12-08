using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Domain.Models;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ChooseMemeWebServer.Infrastructure
{
    public static class WebSocketExtentions
    {
        public static async Task WriteDataToWebSocket(this WebSocket webSocket, WebSocketData data)
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
                receiveResult.CloseStatusDescription, buffer);

            return result;
        }

        public static async Task SendMessageBroadcast(this WebSocketConnectionManager connectionManager, Lobby lobby, WebSocketData payload)
        {
            await connectionManager.SendMessageToServer(lobby, payload);

            await connectionManager.SendMessageBroadcastWithoutServer(lobby, payload);
        }

        public static async Task SendMessageBroadcastWithoutServer(this WebSocketConnectionManager connectionManager, Lobby lobby, WebSocketData payload)
        {
            foreach (var player in lobby.Players)
            {
                await connectionManager.SendMessageToPlayer(player, payload);
            }
        }

        public static async Task SendMessageToPlayer(this WebSocketConnectionManager connectionManager, Player player, WebSocketData payload)
        {
            if (connectionManager.TryGetPlayerConnection(player, out var playerWebSocket))
            {
                await playerWebSocket.WriteDataToWebSocket(payload);
            }
        }

        public static async Task SendMessageToServer(this WebSocketConnectionManager connectionManager, Lobby lobby, WebSocketData payload)
        {
            if(connectionManager.TryGetServerConnection(lobby, out var serverWebSocket))
            {
                await serverWebSocket.WriteDataToWebSocket(payload);
            }
        }
    }
}
