﻿using ChooseMemeWebServer.Interfaces;
using ChooseMemeWebServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace ChooseMemeWebServer
{
    public class ConnectionController : ControllerBase
    {
        private readonly ILobbyService _lobbyService;
        private readonly IWebSocketCommandHandler _commandHandler;

        public ConnectionController(ILobbyService lobbyService, IWebSocketCommandHandler commandHandler)
        {
            _lobbyService = lobbyService;
            _commandHandler = commandHandler;
        }


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

                Player player = new Player()
                {
                    Username = username,
                    WebSocket = webSocket
                };

                Lobby lobby = null!;

                lobby = _lobbyService.ConnectToLobby(lobbyCode, player);

                await ListenClient(player, lobby);
                return Ok();
            }
            else
            {
                return BadRequest("Support only WebSocket request");
            }
        }

        [Route("/wsServer")]
        public async Task<IActionResult> ServerConnect()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                if (!_lobbyService.TryCreateLobby(webSocket))
                {
                    return BadRequest("Error in lobby creation");
                }

                await MaintenanceServerConnection(webSocket);
                return Ok();
            }
            else
            {
                return BadRequest("Support only WebSocket request");
            }
        }

        private async Task ListenClient(Player player, Lobby lobby)
        {
            while (!player.WebSocket.CloseStatus.HasValue)
            {
                var buffer = new byte[1024 * 4];
                var receiveResult = await player.WebSocket.ReceiveAsync(buffer, CancellationToken.None);

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    continue;
                }

                string message = Encoding.UTF8.GetString(buffer);

                _commandHandler.Handle(message, player, lobby);
            }

            await player.WebSocket.CloseAsync(
                player.WebSocket.CloseStatus.Value,
                player.WebSocket.CloseStatusDescription,
                CancellationToken.None);
        }

        private async Task MaintenanceServerConnection(WebSocket webSocket)
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