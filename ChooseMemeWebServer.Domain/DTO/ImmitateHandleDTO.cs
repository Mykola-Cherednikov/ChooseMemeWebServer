using ChooseMemeWebServer.Application.Common.WebSocket;

namespace ChooseMemeWebServer.Application.DTO
{
    public class ImmitateHandleDTO
    {
        public WebSocketRequestMessage Message { get; set; } = null!;

        public string PlayerId { get; set; } = string.Empty;
    }
}
