using System.Net.WebSockets;

namespace ChooseMemeWebServer.Domain
{
    public class BetterWebSocketReceiveResult : WebSocketReceiveResult
    {
        public string Message { get; set; } = string.Empty;

        public BetterWebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage) : base(count, messageType, endOfMessage)
        {
        }

        public BetterWebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string? closeStatusDescription) : base(count, messageType, endOfMessage, closeStatus, closeStatusDescription)
        {
        }
    }
}
