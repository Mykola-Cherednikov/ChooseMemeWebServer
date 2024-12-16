using AutoMapper;
using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using RandomNameGeneratorLibrary;
using System.Text;
using System.Text.Json;

namespace ChooseMemeWebServer.Core.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly IWebSocketSender _sender;
        private readonly IMapper _mapper;
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;

        private static readonly Dictionary<string, Lobby> activeLobbies = new Dictionary<string, Lobby>();

        public LobbyService(IWebSocketSender sender, IMapper mapper, IPlayerService playerService, IGameService gameService)
        {
            _sender = sender;
            _mapper = mapper;
            _playerService = playerService;
            _gameService = gameService;
        }

        public Lobby CreateLobby()
        {
            Lobby lobby = new Lobby(GenerateCode(6));

            activeLobbies.Add(lobby.Code, lobby);

            return lobby;
        }

        public async Task<Lobby> ServerJoinToLobby(Lobby lobby)
        {
            var payload = new WebSocketData(CommandType.CreateLobby, _mapper.Map<LobbyDTO>(lobby));

            await _sender.SendMessageToServer(lobby, payload);

            return lobby;
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

            lobby.PlayerJoin(player);

            var payload = new WebSocketData(CommandType.PlayerJoin, _mapper.Map<PlayerDTO>(player));

            await _sender.SendMessageToServer(lobby, payload);

            payload = new WebSocketData(CommandType.PlayerJoin);

            await _sender.SendMessageToPlayer(player, payload);

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
            var bot = _playerService.AddBot();

            return await JoinToLobby(code, bot);
        }

        public async Task<Lobby> LeaveFromLobby(Player player)
        {
            var lobby = player.Lobby;

            lobby.Players.Remove(player);

            _playerService.RemoveOnlinePlayer(player);

            var payload = new WebSocketData(CommandType.PlayerLeave, _mapper.Map<PlayerDTO>(player));

            await _sender.SendMessageToServer(lobby, payload);

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

            var payload = new WebSocketData(CommandType.NewLeader, _mapper.Map<PlayerDTO>(player));

            await _sender.SendMessageToServer(lobby, payload);
            await _sender.SendMessageToPlayer(player, payload);

            return lobby;
        }

        public async Task ForceStartGame(Player player, Lobby lobby)
        {
            if (!player.IsLeader)
            {
                return;
            }

            var payload = new WebSocketData(CommandType.ForceStartGame);

            await _sender.SendMessageToServer(lobby, payload);
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
