using ChooseMemeWebServer.Application.Common.WebSocket;

namespace ChooseMemeWebServer.Application.DTO
{
    public class ImmitateServerHandleDTO
    {
        public ServerRequestMessage Message { get; set; } = null!;

        public string ServerId { get; set; } = string.Empty;
    }

    public class ImmitatePlayerHandleDTO
    {
        public PlayerRequestMessage Message { get; set; } = null!;

        public string PlayerId { get; set; } = string.Empty;
    }
}
