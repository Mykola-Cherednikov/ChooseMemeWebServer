using AutoMapper;
using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.LobbyService;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using System.Text;

namespace ChooseMemeWebServer.Application.Services
{
    public class LobbyService(IWebSocketSenderService sender, IMapper mapper, IPlayerService playerService) : ILobbyService
    {
        private static readonly Dictionary<string, Lobby> activeLobbies = new Dictionary<string, Lobby>();

        public Lobby CreateLobby()
        {
            Lobby lobby = new Lobby() { Code = GenerateCode(6)};

            activeLobbies.Add(lobby.Code, lobby);

            return lobby;
        }

        public async Task AddServerToLobby(Lobby lobby, Server server)
        {
            lobby.Server = server;

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
        public async Task<Lobby> LeaveFromLobby(LeaveFromLobbyDTO data)
        {
            var lobby = data.Player.Lobby;
            lobby.Players.Remove(data.Player);
            playerService.RemoveOnlinePlayer(data.Player);

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnPlayerLeave, mapper.Map<PlayerDTO>(data.Player));

            await sender.SendMessageToServer(lobby, payload);

            if (data.Player.IsLeader && lobby.Players.Count > 0)
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

        // WebSocket
        public async Task ForceStartGame(ForceStartGameDTO data)
        {
            if (!data.Player.IsLeader)
            {
                return;
            }

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnForceStartGame);

            await sender.SendMessageToServer(data.Player.Lobby, payload);
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
