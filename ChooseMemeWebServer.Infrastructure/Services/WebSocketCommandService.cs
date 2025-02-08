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
    public class WebSocketCommandService(IServiceProvider provider) : IWebSocketCommandService
    {
        private static Dictionary<WebSocketMessageRequestType, CallInfo> _commandToCommandTypeCache = new()
        {
            { WebSocketMessageRequestType.ForceStartGame, new CallInfo(typeof(ILobbyService), "ForceStartGame", typeof(ForceStartGameDTO)) },
            { WebSocketMessageRequestType.PlayerLeave, new CallInfo(typeof(ILobbyService), "LeaveFromLobby", typeof(LeaveFromLobbyDTO)) },
            { WebSocketMessageRequestType.PlayerIsReady, new CallInfo(typeof(IPlayerService), "SetPlayerIsReady", typeof(SetPlayerIsReadyDTO)) }
        };

        public void Handle(WebSocketRequestMessage message, Player player, Lobby lobby)
        {
            using (var scope = provider.CreateScope())
            {

                if (!_commandToCommandTypeCache.TryGetValue(message.Type, out var callInfo))
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

        public void ImmitateHandle(ImmitateHandleDTO data)
        {
            using (var scope = provider.CreateScope())
            {
                var player = scope.ServiceProvider.GetRequiredService<IPlayerService>().GetOnlinePlayer(data.PlayerId);

                if (player == null)
                {
                    return;
                }

                Handle(data.Message, player, player.Lobby);
            }
        }
    }
}
