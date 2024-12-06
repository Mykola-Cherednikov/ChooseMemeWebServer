using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using RandomNameGeneratorLibrary;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;

namespace ChooseMemeWebServer.Core.Services
{
    public class LobbyService : ILobbyService
    {
        private static readonly ConcurrentDictionary<string, Lobby> _lobbies = new ConcurrentDictionary<string, Lobby>();

        private readonly IPlayerService _playerService;

        public LobbyService(IPlayerService playerService)
        {
            _playerService = playerService;
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

        private bool TryJoinToLobby(string code, Player player, out Lobby? lobby)
        {
            if(!_lobbies.TryGetValue(code, out lobby))
            {
                return false;
            }

            lobby = _lobbies[code];

            player.Lobby = lobby;

            lobby.Players.Add(player);

            _playerService.AddOnlinePlayer(player);

            return true;
        }

        public bool TryPlayerJoinToLobby(string code, Player player, out Lobby? lobby)
        {
            return TryJoinToLobby(code, player, out lobby);
        }

        public bool TryBotJoinToLobby(string code, out Lobby? lobby)
        {
            var placeGenerator = new PersonNameGenerator();
            var name = placeGenerator.GenerateRandomFirstName();

            Player bot = new Player()
            {
                Username = "Bot " + name,
                IsBot = true,
            };

            return TryJoinToLobby(code, bot, out lobby);
        }

        public void DisconnectFromLobby(Player player)
        {

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
