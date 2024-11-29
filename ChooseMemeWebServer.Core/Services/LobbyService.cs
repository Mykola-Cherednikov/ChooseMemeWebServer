using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace ChooseMemeWebServer.Core.Services
{
    public class LobbyService : ILobbyService
    {
        private readonly ConcurrentDictionary<string, Lobby> _lobbies;

        public LobbyService()
        {
            _lobbies ??= new ConcurrentDictionary<string, Lobby>();
        }

        public bool TryCreateLobby(WebSocket serverWebSocket)
        {
            string code = GenerateCode(6);

            Lobby lobby = new()
            {
                Code = code,
                Players = new(),
                ServerWebSocket = serverWebSocket
            };

            return _lobbies.TryAdd(code, lobby);
        }

        public Lobby ConnectToLobby(string code, Player player)
        {
            var lobby = _lobbies[code];

            player.Lobby = lobby;

            lobby.Players.Add(player);

            return lobby;
        }

        public bool TryCloseLobby(WebSocket serverWebSocket, Lobby lobby)
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
