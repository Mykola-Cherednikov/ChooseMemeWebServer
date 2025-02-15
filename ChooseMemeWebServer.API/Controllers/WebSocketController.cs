using ChooseMemeWebServer.Application.Common.WebSocket;
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
        IWebSocketRequestService requestService, ILobbyService lobbyService, IServerService serverService, IConfiguration configuration) : ControllerBase
    {
        [Route("/wsPlayer")]
        public async Task PlayerConnect(string username, string lobbyCode)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                if (string.IsNullOrEmpty(username))
                {
                    await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.UserNameIsNullOrEmpty));
                    return;
                }

                if (string.IsNullOrEmpty(lobbyCode))
                {
                    await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.LobbyCodeIsNullOrEmpty));
                    return;
                }

                Player player = playerService.AddOnlinePlayer(username);

                connectionService.AddPlayerConnection(player, webSocket);

                Lobby? lobby = await lobbyService.AddPlayerToLobby(lobbyCode, player);

                if (lobby == null)
                {
                    connectionService.RemovePlayerConnection(player);

                    await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.CantFindLobby));
                    return;
                }

                await ListenPlayer(webSocket, player, lobby);

                await lobbyService.LeaveFromLobby(lobby, player);

                connectionService.RemovePlayerConnection(player);

                playerService.RemoveOnlinePlayer(player);
            }
        }

        private async Task ListenPlayer(WebSocket webSocket, Player player, Lobby lobby)
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
                    var message = JsonSerializer.Deserialize<PlayerRequestMessage>(receiveResult.Message);

                    if (message == null)
                    {
                        throw new Exception("Message is null");
                    }

                    requestService.HandlePlayerRequest(message, player, lobby);
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

        [Route("/wsServer")]
        public async Task ServerConnect(string lobbyCode)
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
                        await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.CantFindLobby));
                        return;
                    }

                    if (possibleLobby.Server != null)
                    {
                        await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.LobbyAlreadyHaveServer));
                        return;
                    }

                    lobby = possibleLobby;
                }
                else
                {
                    lobby = lobbyService.CreateLobby();
                }

                await lobbyService.AddServerToLobby(lobby, server);

                await ListenServer(webSocket, server, lobby);

                await lobbyService.CloseLobby(lobby, server);

                connectionService.RemoveServerConnection(server);

                serverService.RemoveOnlineServer(server);
            }
        }

        private async Task ListenServer(WebSocket webSocket, Server server, Lobby lobby)
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
                    var message = JsonSerializer.Deserialize<ServerRequestMessage>(receiveResult.Message);

                    if (message == null)
                    {
                        throw new Exception("Message is null");
                    }

                    requestService.HandleServerRequest(message, server, lobby);
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
    }
}
