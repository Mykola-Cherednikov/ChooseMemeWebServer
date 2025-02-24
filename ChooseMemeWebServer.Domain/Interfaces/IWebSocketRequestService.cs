using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IWebSocketRequestService
    {
        public Task HandlePlayerRequest(PlayerRequestMessage data, Player player, Lobby lobby);

        public Task ImmitateHandlePlayerRequest(ImmitatePlayerHandleDTO data);

        public Task HandleServerRequest(ServerRequestMessage data, Server player, Lobby lobby);

        public Task ImmitateHandleServerRequest(ImmitateServerHandleDTO data);
    }
}
