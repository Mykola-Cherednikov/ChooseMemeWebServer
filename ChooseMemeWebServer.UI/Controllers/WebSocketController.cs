using ChooseMemeWebServer.Core.Commands.CreateLobby;
using ChooseMemeWebServer.Core.Commands.HandleCommand;
using ChooseMemeWebServer.Core.Commands.JoinLobby;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Extentions;
using ChooseMemeWebServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace ChooseMemeWebServer.UI.Controllers
{
    public class WebSocketController : ControllerBase
    {
        private readonly IMediator _mediator;
        // Change to mediatr

        public WebSocketController(IMediator mediator)
        {
            _mediator = mediator;
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

                JoinLobbyResponse response = await _mediator.Send(new JoinLobbyCommand() { LobbyCode = lobbyCode, Player = player });

                Lobby lobby = response.Lobby;

                if (lobby == null)
                {
                    return BadRequest("Can`t find Lobby");
                }

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

                CreateLobbyResponse response = await _mediator.Send(new CreateLobbyCommand() { WebSocket = webSocket });
                if (response == null || !response.Success)
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
                var receiveResult = await player.WebSocket.ReadFromWebSocket();

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    continue;
                }

                await _mediator.Send(new HandleCommand() { StringCommand = receiveResult.Message, Player = player, Lobby = lobby});
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
