using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.LobbyService.Request;
using ChooseMemeWebServer.Application.DTO.PlayerService.Request;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using ChooseMemeWebServer.Core.Exceptions.PlayerExceptions;
using ChooseMemeWebServer.Core.Exceptions.ServerExceptions;
using ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Numerics;
using System.Text.Json;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class WebSocketRequestService(IServiceProvider provider) : IWebSocketRequestService
    {
        private static Dictionary<PlayerRequestMessageType, CallInfo> playerRequestToCallInfoCache = new()
        {
            { PlayerRequestMessageType.StartGame, new CallInfo(typeof(ILobbyService), "StartGame", typeof(StartGameRequestDTO)) },
            { PlayerRequestMessageType.PlayerIsReady, new CallInfo(typeof(IPlayerService), "SetPlayerIsReady", typeof(SetPlayerIsReadyRequestDTO)) },
            { PlayerRequestMessageType.ChooseMedia, new CallInfo(typeof(IPlayerService), "ChooseMedia", typeof(ChooseMediaRequestDTO)) }
        };

        private static Dictionary<ServerRequestMessageType, CallInfo> serverRequestToCallInfoCache = new()
        {
            { ServerRequestMessageType.NextStatus, new CallInfo(typeof(ILobbyService), "NextStatus", typeof(NextStatusRequestDTO)) }
        };

        public async Task HandlePlayerRequest(PlayerRequestMessage message, Player player, Lobby lobby)
        {
            using (var scope = provider.CreateScope())
            {
                if (!playerRequestToCallInfoCache.TryGetValue(message.Type, out var callInfo))
                {
                    throw new CallInfoNotFoundException(message.MessageTypeName);

				}

                var classInstance = scope.ServiceProvider.GetService(callInfo.Class);

                if (classInstance == null)
                {
                    throw new InstanceNotFoundException(callInfo.Class.Name);

				}

                var data = JsonSerializer.Deserialize(string.IsNullOrEmpty(message.WebSocketData) ? "{}" : message.WebSocketData, callInfo.DataType) as IPlayerWebSocketRequestData;

                if (data == null)
                {
                    throw new DataIsEmptyException();

				}

                data.Player = player;
                data.Lobby = lobby;

                var result = callInfo.Method.Invoke(classInstance, [data]);

                if (result is Task task)
                {
                    await task;
                }
            }
        }

        public async Task HandleServerRequest(ServerRequestMessage message, Server server, Lobby lobby)
        {
            using (var scope = provider.CreateScope())
            {
                if (!serverRequestToCallInfoCache.TryGetValue(message.Type, out var callInfo))
                {
                    throw new CallInfoNotFoundException(message.MessageTypeName);
                }

                var classInstance = scope.ServiceProvider.GetService(callInfo.Class);

                if (classInstance == null)
                {
                    throw new InstanceNotFoundException(callInfo.Class.Name);
                }

                var data = JsonSerializer.Deserialize(string.IsNullOrEmpty(message.WebSocketData) ? "{}" : message.WebSocketData, callInfo.DataType) as IServerWebSocketRequestData;

                if (data == null)
                {
                    throw new DataIsEmptyException();
                }

                data.Server = server;
                data.Lobby = lobby;

                var result = callInfo.Method.Invoke(classInstance, [data]);

                if (result is Task task)
                {
                    await task;
                }
            }
        }

        public async Task ImmitateHandlePlayerRequest(ImmitatePlayerHandleDTO data)
        {
            using (var scope = provider.CreateScope())
            {
                var player = scope.ServiceProvider.GetRequiredService<IPlayerService>().GetOnlinePlayer(data.PlayerId);

                if (player == null)
                {
                    throw new PlayerNotFoundException();
				}

                await HandlePlayerRequest(data.Message, player, player.Lobby);
            }
        }

        public async Task ImmitateHandleServerRequest(ImmitateServerHandleDTO data)
        {
            using (var scope = provider.CreateScope())
            {
                var server = scope.ServiceProvider.GetRequiredService<IServerService>().GetOnlineServer(data.ServerId);

                if (server == null)
                {
                    throw new ServerNotFoundException();

				}

                await HandleServerRequest(data.Message, server, server.Lobby);
            }
        }
    }
}
