using ChooseMemeWebServer.Core.Commands.PlayerCommands.HandlePlayerCommand;
using ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreateLobbyFromWebSocket;
using ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreatePlayer;
using ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.PlayerJoinLobby;
using ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.ServerJoinLobby;
using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Domain.Models;
using ChooseMemeWebServer.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text.Json;

namespace ChooseMemeWebServer.UI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class WebSocketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly WebSocketConnectionManager _connectionManager;

        public WebSocketController(IMediator mediator, WebSocketConnectionManager connectionManager)
        {
            _mediator = mediator;
            _connectionManager = connectionManager;
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

                CreatePlayerResponse playerResponse = new CreatePlayerResponse();

                Player player = playerResponse.Player;

                _connectionManager.AddPlayerConnection(player, webSocket);

                PlayerJoinLobbyResponse lobbyResponse = await _mediator.Send(new PlayerJoinLobbyCommand() { LobbyCode = lobbyCode, Player = player });

                Lobby? lobby = lobbyResponse.Lobby;

                if (lobby == null)
                {
                    _connectionManager.RemovePlayerConnection(player);

                    return BadRequest("Can`t find Lobby");
                }

                await ListenClient(webSocket, player, lobby);

                _connectionManager.RemovePlayerConnection(player);

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

                CreateLobbyFromWebSocketResponse response = await _mediator.Send(new CreateLobbyFromWebSocketCommand());

                _connectionManager.AddServerConnection(response.Lobby, webSocket);

                await _mediator.Send(new ServerJoinLobbyCommand() { Lobby = response.Lobby });

                await MaintenanceServerConnection(webSocket);

                _connectionManager.RemoveServerConnection(response.Lobby);

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

                await _mediator.Send(new HandlePlayerCommandCommand() { WebSocketData = receiveResult.Message, Player = player, Lobby = lobby });
            }

            await webSocket.CloseAsync(
                webSocket.CloseStatus.Value,
                webSocket.CloseStatusDescription,
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
