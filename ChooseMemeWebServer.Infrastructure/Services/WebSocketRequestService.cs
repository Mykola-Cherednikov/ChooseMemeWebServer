using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.LobbyService;
using ChooseMemeWebServer.Application.DTO.PlayerService;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class WebSocketRequestService(IServiceProvider provider) : IWebSocketRequestService
    {
        private static Dictionary<PlayerRequestMessageType, CallInfo> _requestToCallInfoCache = new()
        {
            { PlayerRequestMessageType.ForceStartGame, new CallInfo(typeof(ILobbyService), "ForceStartGame", typeof(ForceStartGameDTO)) },
            { PlayerRequestMessageType.PlayerLeave, new CallInfo(typeof(ILobbyService), "LeaveFromLobby", typeof(LeaveFromLobbyDTO)) },
            { PlayerRequestMessageType.PlayerIsReady, new CallInfo(typeof(IPlayerService), "SetPlayerIsReady", typeof(SetPlayerIsReadyDTO)) }
        };

        public void HandlePlayerRequest(PlayerRequestMessage message, Player player, Lobby lobby)
        {
            using (var scope = provider.CreateScope())
            {

                if (!_requestToCallInfoCache.TryGetValue(message.Type, out var callInfo))
                {
                    return;
                }

                var classInstance = scope.ServiceProvider.GetService(callInfo.Class);

                if (classInstance == null)
                {
                    return;
                }

                var data = JsonSerializer.Deserialize(string.IsNullOrEmpty(message.WebSocketData) ? "{}" : message.WebSocketData, callInfo.DataType) as IWebSocketData;

                if (data == null)
                {
                    return;
                }

                data.Player = player;
                data.Lobby = lobby;

                callInfo.Method.Invoke(classInstance, [data]);
            }
        }

        public void HandleServerRequest(PlayerRequestMessage data, Server server, Lobby lobby)
        {
            throw new NotImplementedException();
        }

        public void ImmitateHandlePlayerRequest(ImmitatePlayerHandleDTO data)
        {
            using (var scope = provider.CreateScope())
            {
                var player = scope.ServiceProvider.GetRequiredService<IPlayerService>().GetOnlinePlayer(data.PlayerId);

                if (player == null)
                {
                    return;
                }

                HandlePlayerRequest(data.Message, player, player.Lobby);
            }
        }

        public void ImmitateHandleServerRequest(ImmitateServerHandleDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
