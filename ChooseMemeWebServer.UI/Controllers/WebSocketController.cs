using ChooseMemeWebServer.Core.Commands.PlayerCommands.HandlePlayerCommand;
using ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreateLobbyWithServer;
using ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.PlayerJoinLobby;
using ChooseMemeWebServer.Domain;
using ChooseMemeWebServer.Domain.Extentions;
using ChooseMemeWebServer.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChooseMemeWebServer.UI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class WebSocketController : ControllerBase
    {
        private readonly IMediator _mediator;

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
                    Id = Guid.NewGuid().ToString(),
                    Username = username,
                    WebSocket = webSocket
                };

                PlayerJoinLobbyResponse response = await _mediator.Send(new PlayerJoinLobbyCommand() { LobbyCode = lobbyCode, Player = player });

                Lobby lobby = response.Lobby;

                if (lobby == null)
                {
                    return BadRequest("Can`t find Lobby");
                }

                // TODO: Send Lobby join

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

                CreateLobbyWithServerResponse response = await _mediator.Send(new CreateLobbyWithServerCommand() { WebSocket = webSocket });

                if (response == null || !response.Success)
                {
                    return BadRequest("Error in lobby creation");
                }

                Lobby lobby = response.Lobby;

                var payload = new WebSocketData() { CommandTypeName = "CreateLobbyWithServerResponse", Data = JsonSerializer.Serialize(response.LobbyDTO) };
                await lobby.WriteDataToLobbyServer(payload);

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
                var receiveResult = await player.WebSocket.ReadDataFromWebSocket();

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    continue;
                }

                await _mediator.Send(new HandlePlayerCommandCommand() { WebSocketData = receiveResult.Message, Player = player, Lobby = lobby });
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
