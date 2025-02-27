using ChooseMemeWebServer.Application.Common.WebSocket;

namespace ChooseMemeWebServer.Application.DTO.PlayerService.Request
{
    public class ChooseMediaRequestDTO : BasePlayerWebSocketRequestData
    {
        public string MediaId { get; set; } = string.Empty;
    }
}
