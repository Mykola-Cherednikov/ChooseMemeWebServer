using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ChooseMemeWebServer.Domain.Extentions
{
    public static class WebSocketExtentions
    {
        public static async Task WriteToWebSocket(this WebSocket webSocket, WebSocketData data)
        {
            string json = JsonSerializer.Serialize(data);

            var buffer = Encoding.UTF8.GetBytes(json);

            await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public static async Task<BetterWebSocketReceiveResult> ReadFromWebSocket(this WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            BetterWebSocketReceiveResult receiveResult = (BetterWebSocketReceiveResult)await webSocket.ReceiveAsync(buffer, CancellationToken.None);

            if (receiveResult.MessageType == WebSocketMessageType.Text)
            {
                receiveResult.Message = Encoding.UTF8.GetString(buffer);
            }

            return receiveResult;
        }
    }
}
