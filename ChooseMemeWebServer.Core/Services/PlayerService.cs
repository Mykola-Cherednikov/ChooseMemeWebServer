using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using System.Collections.Concurrent;

namespace ChooseMemeWebServer.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private static readonly ConcurrentDictionary<string, Player> _onlinePlayers = new ConcurrentDictionary<string, Player>();

        public List<Player> GetOnlinePlayers()
        {
            return _onlinePlayers.Values.ToList();
        }

        public Player GetPlayer(string playerId)
        {
            return _onlinePlayers[playerId];
        }

        public void AddOnlinePlayer(Player player)
        {
            player.Id = Guid.NewGuid().ToString();

            _onlinePlayers.TryAdd(player.Id, player);
        }

        public void RemoveOnlinePlayer(string id)
        {
            _onlinePlayers.TryRemove(id, out var player);
        }
    }
}
