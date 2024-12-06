using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain;
using ChooseMemeWebServer.Domain.Extentions;
using ChooseMemeWebServer.Domain.Models;
using RandomNameGeneratorLibrary;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ChooseMemeWebServer.Core.Services
{
    public class LobbyService : ILobbyService
    {
        private static readonly Dictionary<string, Lobby> _lobbies = new Dictionary<string, Lobby>();

        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public LobbyService(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        public Lobby? GetLobby(string code)
        {
            return _lobbies.TryGetValue(code, out var lobby) ? lobby : null;
        }

        public List<Lobby> GetLobbies()
        {
            return _lobbies.Values.ToList();
        }

        public Lobby CreateLobby()
        {
            string code = GenerateCode(6);

            Lobby lobby = new()
            {
                Code = code,
                Players = new()
            };

            _lobbies.TryAdd(lobby.Code, lobby);

            return lobby;
        }

        public Lobby CreateLobbyWithServer(WebSocket serverWebSocket)
        {
            var lobby = CreateLobby();

            lobby.ServerWebSocket = serverWebSocket;

            return lobby;
        }

        private async Task<Lobby?> JoinToLobby(string code, Player player)
        {
            if (!_lobbies.TryGetValue(code, out var lobby))
            {
                return null;
            }

            lobby = _lobbies[code];

            player.Lobby = lobby;

            lobby.Players.Add(player);

            _playerService.AddOnlinePlayer(player);

            var payload = new WebSocketData() { CommandTypeName = "PlayerJoin", Data = JsonSerializer.Serialize(_mapper.Map<PlayerDTO>(player)) };
            await lobby.WriteDataToLobbyServer(payload);

            var leader = lobby.Players.FirstOrDefault(p => p.IsLeader);
            if (leader == null)
            {
                await SetLeader(player, lobby);
            }

            return lobby;
        }

        public async Task<Lobby?> PlayerJoinToLobby(string code, Player player)
        {
            return await JoinToLobby(code, player);
        }

        public async Task<Lobby?> BotJoinToLobby(string code)
        {
            var placeGenerator = new PersonNameGenerator();
            var name = placeGenerator.GenerateRandomFirstName();

            Player bot = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Username = "Bot " + name,
                IsBot = true,
            };

            return await JoinToLobby(code, bot);
        }

        public async Task LeaveFromLobby(Player player)
        {
            var lobby = player.Lobby;

            lobby.Players.Remove(player);

            _playerService.RemoveOnlinePlayer(player.Id);

            var payload = new WebSocketData { CommandTypeName = "PlayerLeave", Data = JsonSerializer.Serialize(_mapper.Map<PlayerDTO>(player)) };
            await lobby.WriteDataToLobbyServer(payload);

            if (player.IsLeader && lobby.Players.Count > 0)
            {
                var newLeader = lobby.Players[0];
                await SetLeader(newLeader, lobby);
            }
        }

        private async Task SetLeader(Player player, Lobby lobby)
        {
            player.IsLeader = true;
            var payload = new WebSocketData { CommandTypeName = "NewLeader", Data = JsonSerializer.Serialize(_mapper.Map<PlayerDTO>(player)) };
            await lobby.WriteDataToLobbyServer(payload);
            await player.WriteDataToPlayerWebSocket(payload);
        }

        public void CloseLobby(string code)
        {
            throw new NotImplementedException();
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
    }
}
