using ChooseMemeWebServer.Application.Common.WebSocket;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ChooseMemeWebServer.Infrastructure.Extensions
{
    public static class WebSocketExtensions
    {
        public static async Task WriteDataToWebSocket(this WebSocket webSocket, WebSocketResponseMessage data)
        {
            if (webSocket.State == WebSocketState.Closed)
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
    }
}
