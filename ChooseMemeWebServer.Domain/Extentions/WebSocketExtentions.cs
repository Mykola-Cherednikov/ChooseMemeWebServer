using ChooseMemeWebServer.Domain.Models;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace ChooseMemeWebServer.Domain.Extentions
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
            BetterWebSocketReceiveResult receiveResult = (BetterWebSocketReceiveResult)await webSocket.ReceiveAsync(buffer, CancellationToken.None);

            if (receiveResult.MessageType == WebSocketMessageType.Text)
            {
                receiveResult.Message = Encoding.UTF8.GetString(buffer);
            }

            return receiveResult;
        }

        public static async Task BroadcastDataToLobby(this Lobby lobby, WebSocketData data)
        {
            foreach (var player in lobby.Players)
            {
                await player.WriteDataToPlayerWebSocket(data);
            }
        }

        public static async Task WriteDataToPlayerWebSocket(this Player player, WebSocketData data)
        {
            if (player.WebSocket != null)
            {
                await player.WebSocket.WriteDataToWebSocket(data);
            }
        }

        public static async Task WriteDataToLobbyServer(this Lobby lobby, WebSocketData data)
        {
            if (lobby.ServerWebSocket != null)
            {
                await lobby.ServerWebSocket.WriteDataToWebSocket(data);
            }
        }
    }
}
