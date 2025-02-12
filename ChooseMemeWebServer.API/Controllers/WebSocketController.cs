﻿using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using ChooseMemeWebServer.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text.Json;

namespace ChooseMemeWebServer.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class WebSocketController(IWebSocketConnectionService connectionService, IPlayerService playerService,
        IWebSocketCommandService commandService, ILobbyService lobbyService, IServerService serverService, IConfiguration configuration) : ControllerBase
    {
        [Route("/wsClient")]
        public async Task<IActionResult> ClientConnect(string username, string lobbyCode)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username is null or empty");
                }

                if (string.IsNullOrEmpty(lobbyCode))
                {
                    return BadRequest("LobbyCode is null or empty");
                }

                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                Player player = playerService.AddOnlinePlayer(username);

                connectionService.AddPlayerConnection(player, webSocket);

                Lobby? lobby = await lobbyService.AddPlayerToLobby(lobbyCode, player);

                if (lobby == null)
                {
                    connectionService.RemovePlayerConnection(player);

                    return BadRequest("Can`t find Lobby");
                }

                await ListenClient(webSocket, player, lobby);

                connectionService.RemovePlayerConnection(player);

                return Ok();
            }
            else
            {
                return BadRequest("Support only WebSocket request");
            }
        }

        [Route("/wsServer")]
        public async Task<IActionResult> ServerConnect(string lobbyCode)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                Server server = serverService.AddOnlineServer();

                connectionService.AddServerConnection(server, webSocket);

                Lobby lobby;

                if (bool.TryParse(configuration["IsTesting"], out bool isTesting) && isTesting && !string.IsNullOrEmpty(lobbyCode))
                {
                    var possibleLobby = lobbyService.GetLobby(lobbyCode);

                    if (possibleLobby == null)
                    {
                        return BadRequest("Lobby not found");
                    }

                    if (possibleLobby.Server != null)
                    {
                        return BadRequest($"Lobby already have server");
                    }

                    lobby = possibleLobby;
                }
                else
                {
                    lobby = lobbyService.CreateLobby();
                }

                await lobbyService.AddServerToLobby(lobby, server);

                await ListenServer(webSocket, server, lobby);

                connectionService.RemoveServerConnection(server);

                serverService.RemoveOnlineServer(server);

                return Ok();
            }
            else
            {
                return BadRequest("Support only WebSocket request");
            }
        }

        private async Task ListenClient(WebSocket webSocket, Player player, Lobby lobby)
        {
            while (!webSocket.CloseStatus.HasValue)
            {
                var receiveResult = await webSocket.ReadDataFromWebSocket();

                if (receiveResult.MessageType == BetterWebSocketMessageType.Close)
                {
                    continue;
                }

                try
                {
                    var message = JsonSerializer.Deserialize<WebSocketRequestMessage>(receiveResult.Message);

                    if (message == null)
                    {
                        throw new Exception("Message is null");
                    }

                    commandService.Handle(message, player, lobby);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }

            await webSocket.CloseAsync(
                webSocket.CloseStatus.Value,
                webSocket.CloseStatusDescription,
                CancellationToken.None);
        }

        private async Task ListenServer(WebSocket webSocket, Server server, Lobby lobby)
        {
            while (!webSocket.CloseStatus.HasValue)
            {
                await Task.Delay(1000);
            }

            await webSocket.CloseAsync(
                webSocket.CloseStatus.Value,
                webSocket.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}
