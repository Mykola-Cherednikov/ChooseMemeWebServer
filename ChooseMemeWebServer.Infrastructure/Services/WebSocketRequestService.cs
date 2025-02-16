using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.LobbyService;
using ChooseMemeWebServer.Application.DTO.PlayerService;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Numerics;
using System.Text.Json;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class WebSocketRequestService(IServiceProvider provider) : IWebSocketRequestService
    {
        private static Dictionary<PlayerRequestMessageType, CallInfo> _playerRequestToCallInfoCache = new()
        {
            { PlayerRequestMessageType.StartGame, new CallInfo(typeof(ILobbyService), "StartGame", typeof(StartGameDTO)) },
            { PlayerRequestMessageType.PlayerIsReady, new CallInfo(typeof(IPlayerService), "SetPlayerIsReady", typeof(SetPlayerIsReadyDTO)) }
        };

        private static Dictionary<ServerRequestMessageType, CallInfo> _serverRequestToCallInfoCache = new()
        {
            { ServerRequestMessageType.NextStatus, new CallInfo(typeof(ILobbyService), "NextStatus", typeof(NextStatusDTO)) }
        };

        public void HandlePlayerRequest(PlayerRequestMessage message, Player player, Lobby lobby)
        {
            using (var scope = provider.CreateScope())
            {

                if (!_playerRequestToCallInfoCache.TryGetValue(message.Type, out var callInfo))
                {
                    return;
                }

                var classInstance = scope.ServiceProvider.GetService(callInfo.Class);

                if (classInstance == null)
                {
                    return;
                }

                var data = JsonSerializer.Deserialize(string.IsNullOrEmpty(message.WebSocketData) ? "{}" : message.WebSocketData, callInfo.DataType) as IPlayerWebSocketData;

                if (data == null)
                {
                    return;
                }

                data.Player = player;
                data.Lobby = lobby;

                callInfo.Method.Invoke(classInstance, [data]);
            }
        }

        public void HandleServerRequest(ServerRequestMessage message, Server server, Lobby lobby)
        {
            using (var scope = provider.CreateScope())
            {
                if (!_serverRequestToCallInfoCache.TryGetValue(message.Type, out var callInfo))
                {
                    return;
                }

                var classInstance = scope.ServiceProvider.GetService(callInfo.Class);

                if (classInstance == null)
                {
                    return;
                }

                var data = JsonSerializer.Deserialize(string.IsNullOrEmpty(message.WebSocketData) ? "{}" : message.WebSocketData, callInfo.DataType) as IServerWebSocketData;

                if (data == null)
                {
                    return;
                }

                data.Server = server;
                data.Lobby = lobby;

                callInfo.Method.Invoke(classInstance, [data]);
            }
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
            using (var scope = provider.CreateScope())
            {
                var server = scope.ServiceProvider.GetRequiredService<IServerService>().GetOnlineServer(data.ServerId);

                if (server == null)
                {
                    return;
                }

                HandleServerRequest(data.Message, server, server.Lobby);
            }
        }
    }
}
