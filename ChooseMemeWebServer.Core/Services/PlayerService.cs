using AutoMapper;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain;
using ChooseMemeWebServer.Domain.Extentions;
using ChooseMemeWebServer.Domain.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace ChooseMemeWebServer.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private static readonly Dictionary<string, Player> _onlinePlayers = new Dictionary<string, Player>();

        private readonly IMapper _mapper;

        public PlayerService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<Player> GetOnlinePlayers()
        {
            return _onlinePlayers.Values.ToList();
        }

        public Player? GetPlayer(string playerId)
        {
            return _onlinePlayers.TryGetValue(playerId, out Player? player) ? player : null;
        }

        public void AddOnlinePlayer(Player player)
        {
            _onlinePlayers.TryAdd(player.Id, player);
        }

        public void RemoveOnlinePlayer(string id)
        {
            _onlinePlayers.Remove(id);
        }

        public void SetPlayerIsReady(Player player)
        {
            player.IsReady = true;

            var payload = new WebSocketData() { CommandTypeName = "PlayerIsReady", Data = JsonSerializer.Serialize(_mapper.Map<PlayerDTO>(player)) };

            player.Lobby.WriteDataToLobbyServer(payload).GetAwaiter().GetResult();
        }
    }
}
