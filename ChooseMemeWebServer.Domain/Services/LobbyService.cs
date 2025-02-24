﻿using AutoMapper;
using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.LobbyService;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Dynamic;

namespace ChooseMemeWebServer.Application.Services
{
    public class LobbyService(IWebSocketSenderService sender, IMapper mapper, IPlayerService playerService, IConfiguration configuration) : ILobbyService
    {
        private static readonly ConcurrentDictionary<string, Lobby> activeLobbies = new ConcurrentDictionary<string, Lobby>();

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

            for (int i = 0; i < 8; i++)
            {
                lobby.StatusQueue.Enqueue(LobbyStatus.AskQuestion);
                lobby.StatusQueue.Enqueue(LobbyStatus.AnswerQuestion);
                lobby.StatusQueue.Enqueue(LobbyStatus.QuestionResults);
            }

            lobby.StatusQueue.Enqueue(LobbyStatus.EndResult);
            lobby.StatusQueue.Enqueue(LobbyStatus.End);

            activeLobbies.TryAdd(lobby.Code, lobby);

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
            activeLobbies.TryRemove(lobby.Code, out _);
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
                await playerService.SetLeader(player, lobby);
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
                await playerService.SetLeader(newLeader, lobby);
            }

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
        public async Task NextStatus(NextStatusDTO data)
        {
            var status = data.Lobby.StatusQueue.Dequeue().ToString();

            var instance = this;
            var method = instance.GetType().GetMethod(status);

            if (method == null)
            {
                return;
            }

            var result = method.Invoke(instance, [data]);

            if (result is Task task)
            {
                await task;
            }
        }

        private async Task AskQuestion(NextStatusDTO data)
        {
            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.AskQuestion);

            data.Lobby.Status = LobbyStatus.AskQuestion;
            await sender.SendMessageToServer(data.Lobby, payload);
            await sender.SendMessageToAllPlayers(data.Lobby, payload);
        }

        private void AnswerQuestion(NextStatusDTO data)
        {

        }

        private void ResultsQuestion(NextStatusDTO data)
        {

        }
    }
}
