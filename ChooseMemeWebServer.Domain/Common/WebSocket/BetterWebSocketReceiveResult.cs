using System.Text;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public class BetterWebSocketReceiveResult
    {
        public BetterWebSocketReceiveResult(int count,
            BetterWebSocketMessageType messageType,
            bool endOfMessage,
            BetterWebSocketCloseStatus? closeStatus,
            string? closeStatusDescription,
            byte[] bytes)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            Count = count;
            EndOfMessage = endOfMessage;
            MessageType = messageType;
            CloseStatus = closeStatus;
            CloseStatusDescription = closeStatusDescription;
            Message = Encoding.UTF8.GetString(bytes);
        }

        public int Count { get; }
        public bool EndOfMessage { get; }
        public BetterWebSocketMessageType MessageType { get; }
        public BetterWebSocketCloseStatus? CloseStatus { get; }
        public string? CloseStatusDescription { get; }
        public string Message { get; }
    }

    public enum BetterWebSocketMessageType
    {
        Text = 0,
        Binary = 1,
        Close = 2
    }

    public enum BetterWebSocketCloseStatus
    {
        NormalClosure = 1000,
        EndpointUnavailable = 1001,
        ProtocolError = 1002,
        InvalidMessageType = 1003,
        Empty = 1005,
        InvalidPayloadData = 1007,
        PolicyViolation = 1008,
        MessageTooBig = 1009,
        MandatoryExtension = 1010,
        InternalServerError = 1011
    }
}
