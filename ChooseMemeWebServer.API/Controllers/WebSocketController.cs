using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core.Entities;
using ChooseMemeWebServer.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text.Json;

namespace ChooseMemeWebServer.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class WebSocketController(IWebSocketConnectionService connectionService, IPlayerService playerService, 
        IWebSocketCommandService commandService, ILobbyService lobbyService) : ControllerBase
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
        public async Task<IActionResult> ServerConnect()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                Lobby lobby = lobbyService.CreateLobby();

                connectionService.AddServerConnection(lobby, webSocket);

                await lobbyService.ServerJoinToLobby(lobby);

                await ListenServer(webSocket);

                connectionService.RemoveServerConnection(lobby);

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

                    if(message == null)
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

        private async Task ListenServer(WebSocket webSocket)
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
