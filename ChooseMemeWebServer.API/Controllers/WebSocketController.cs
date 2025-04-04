﻿using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.Exceptions;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using ChooseMemeWebServer.Core.Exceptions.LobbyExceptions;
using ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions;
using ChooseMemeWebServer.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text.Json;

namespace ChooseMemeWebServer.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class WebSocketController(IWebSocketConnectionService connectionService, IWebSocketSenderService senderService, IPlayerService playerService,
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
                    await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.UsernameIsNullOrEmpty));
                    throw new UsernameIsNullOrEmptyException();

                }

                if (string.IsNullOrEmpty(lobbyCode))
                {
                    await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.LobbyCodeIsNullOrEmpty));
                    throw new LobbyCodeIsNullOrEmptyException();

                }

                Player player = playerService.AddOnlinePlayer(username);

                connectionService.AddPlayerConnection(player, webSocket);

                try
                {
                    Lobby lobby = await lobbyService.AddPlayerToLobby(lobbyCode, player);

                    await ListenPlayer(webSocket, player, lobby);

                    await lobbyService.LeaveFromLobby(lobby, player);

                    connectionService.RemovePlayerConnection(player);

                    playerService.RemoveOnlinePlayer(player);
                }
                catch (LobbyNotFoundException ex)
                {
                    connectionService.RemovePlayerConnection(player);

                    await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.CantFindLobby));

                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private async Task ListenPlayer(WebSocket webSocket, Player player, Lobby lobby)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                string json = "";

                try
                {
                    var receiveResult = await webSocket.ReadDataFromWebSocket();
                    json = receiveResult.Message;

                    if (receiveResult.MessageType == BetterWebSocketMessageType.Close)
                    {
                        continue;
                    }

                    var message = JsonSerializer.Deserialize<PlayerRequestMessage>(receiveResult.Message);

                    if (message == null)
                    {
                        throw new ExpectedException("Message is null");
                    }

                    await requestService.HandlePlayerRequest(message, player, lobby);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Json error: " + json);
                    Console.WriteLine(ex.ToString());
                }
                catch (ExpectedException ex)
                {
                    var message = new MessageDTO()
                    {
                        Message = ex.Message.ToString()
                    };
                    var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.ExpectedError, message);
                    await senderService.SendMessageToPlayer(player, payload);
                    Console.WriteLine("Expected: " + ex.ToString());
                }
                catch (WebSocketException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }

            if (webSocket.State == WebSocketState.Open || webSocket.State == WebSocketState.CloseReceived || webSocket.State == WebSocketState.CloseSent)
            {
                await webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    webSocket.CloseStatusDescription,
                    CancellationToken.None);
            }
        }

        [Route("/wsServer")]
        public async Task ServerConnect(string presetId, string lobbyCode)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                try
                {
                    Server server = serverService.AddOnlineServer();

                    connectionService.AddServerConnection(server, webSocket);

                    Lobby lobby;

                    if (bool.TryParse(configuration["IsTesting"], out bool isTesting) && isTesting && !string.IsNullOrEmpty(lobbyCode))
                    {
                        var possibleLobby = lobbyService.GetLobby(lobbyCode);
                        lobby = possibleLobby;
                    }
                    else
                    {
                        lobby = await lobbyService.CreateLobby(presetId);
                    }

                    await lobbyService.AddServerToLobby(lobby, server);

                    await ListenServer(webSocket, server, lobby);

                    await lobbyService.CloseLobby(lobby, server);

                    connectionService.RemoveServerConnection(server);

                    serverService.RemoveOnlineServer(server);
                }
                catch (LobbyNotFoundException ex)
                {
                    await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.CantFindLobby));
                    Console.WriteLine(ex.ToString());
                }
                catch (LobbyAlreadyHaveServerException ex)
                {
                    await webSocket.WriteDataToWebSocket(new WebSocketResponseMessage(WebSocketMessageResponseType.LobbyAlreadyHaveServer));
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private async Task ListenServer(WebSocket webSocket, Server server, Lobby lobby)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                string json = "";

                try
                {
                    var receiveResult = await webSocket.ReadDataFromWebSocket();
                    json = receiveResult.Message;

                    if (receiveResult.MessageType == BetterWebSocketMessageType.Close)
                    {
                        continue;
                    }

                    var message = JsonSerializer.Deserialize<ServerRequestMessage>(receiveResult.Message);

                    if (message == null)
                    {
                        throw new ExpectedException("Message is null");
                    }

                    await requestService.HandleServerRequest(message, server, lobby);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Json error: " + json);
                    Console.WriteLine(ex.ToString());
                }
                catch (ExpectedException ex)
                {
                    var message = new MessageDTO()
                    {
                        Message = ex.Message.ToString()
                    };
                    var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.ExpectedError, message);
                    await senderService.SendMessageToServer(server, payload);
                    Console.WriteLine("Expected: " + ex.ToString());
                }
                catch (WebSocketException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }

            if (webSocket.State == WebSocketState.Open || webSocket.State == WebSocketState.CloseReceived || webSocket.State == WebSocketState.CloseSent)
            {
                await webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    webSocket.CloseStatusDescription,
                    CancellationToken.None);
            }
        }
    }
}
