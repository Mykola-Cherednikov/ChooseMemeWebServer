﻿using AutoMapper;
using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.LobbyService;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ChooseMemeWebServer.Application.Services
{
    public class LobbyService(IWebSocketSenderService sender, IMapper mapper, IPlayerService playerService, IConfiguration configuration) : ILobbyService
    {
        private static readonly Dictionary<string, Lobby> activeLobbies = new Dictionary<string, Lobby>();

        public Lobby CreateLobby()
        {
            Lobby lobby;

            if (activeLobbies.Count == 0 && bool.TryParse(configuration["IsTesting"], out bool isTesting) && isTesting)
            {
                lobby = new Lobby() { Code = "AAAAAA" };
            }
            else
            {
                lobby = new Lobby() { Code = GenerateCode(6) };
            }

            activeLobbies.Add(lobby.Code, lobby);

            return lobby;
        }

        // WebSocket
        public async Task CloseLobby(Lobby lobby, Server server)
        {
            if (lobby.Server != server)
            {
                return;
            }

            foreach(var bot in lobby.Players.Where(p => p.IsBot).ToList())
            {
                playerService.RemoveOnlinePlayer(bot);
                await LeaveFromLobby(lobby, bot);
            }

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnLobbyClose);

            await sender.SendMessageBroadcast(lobby, payload);

            lobby.Server = null!;
            server.Lobby = null!;
            activeLobbies.Remove(lobby.Code);
        }

        public async Task AddServerToLobby(Lobby lobby, Server server)
        {
            lobby.Server = server;

            server.Lobby = lobby;

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnCreateLobby, mapper.Map<LobbyDTO>(lobby));

            await sender.SendMessageToServer(lobby, payload);
        }

        public List<Lobby> GetLobbies()
        {
            return activeLobbies.Values.ToList();
        }

        public Lobby? GetLobby(string code)
        {
            activeLobbies.TryGetValue(code, out var lobby);
            return lobby;
        }

        private async Task<Lobby?> JoinToLobby(string code, Player player)
        {
            if (!activeLobbies.TryGetValue(code, out var lobby))
            {
                return null;
            }

            lobby.Players.Add(player);

            player.Lobby = lobby;

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnPlayerJoin, mapper.Map<PlayerDTO>(player));

            await sender.SendMessageToServer(lobby, payload);

            payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnPlayerJoin);

            await sender.SendMessageToPlayer(player, payload);

            var leader = lobby.Players.FirstOrDefault(p => p.IsLeader);

            if (leader == null)
            {
                await SetLeader(player, lobby);
            }

            return lobby;
        }

        public async Task<Lobby?> AddPlayerToLobby(string code, Player player)
        {
            return await JoinToLobby(code, player);
        }

        public async Task<Lobby?> AddBotToLobby(string code)
        {
            var bot = playerService.AddBot();

            return await JoinToLobby(code, bot);
        }


        //WebSocket
        public async Task<Lobby> LeaveFromLobby(Lobby lobby, Player player)
        {
            player.Lobby = null!;
            lobby.Players.Remove(player);

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnPlayerLeave, mapper.Map<PlayerDTO>(player));

            await sender.SendMessageToServer(lobby, payload);

            if (player.IsLeader && lobby.Players.Count > 0)
            {
                var newLeader = lobby.Players[0];
                await SetLeader(newLeader, lobby);
            }

            return lobby;
        }

        public async Task<Lobby> SetLeader(Player player, Lobby lobby)
        {
            player.IsLeader = true;

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.NewLeader, mapper.Map<PlayerDTO>(player));

            await sender.SendMessageToServer(lobby, payload);
            await sender.SendMessageToPlayer(player, payload);

            return lobby;
        }  

        private string GenerateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            StringBuilder stringBuilder = new();

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        // WebSocket
        public async Task StartGame(StartGameDTO data)
        {
            if (!data.Player.IsLeader)
            {
                return;
            }

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnStartGame);

            data.Lobby.Status = LobbyStatus.GameStart;
            await sender.SendMessageToServer(data.Player.Lobby, payload);
            await sender.SendMessageToAllPlayers(data.Player.Lobby, payload);
        }

        // WebSocket Server
        public async Task NextStatus()
        {

        }
    }
}
