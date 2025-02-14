using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IWebSocketRequestService
    {
        public void HandlePlayerRequest(PlayerRequestMessage data, Player player, Lobby lobby);

        public void ImmitateHandlePlayerRequest(ImmitatePlayerHandleDTO data);

        public void HandleServerRequest(ServerRequestMessage data, Server player, Lobby lobby);

        public void ImmitateHandleServerRequest(ImmitateServerHandleDTO data);
    }
}
