using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface IPlayerService
    {
        public List<Player> GetOnlinePlayers();

        public Player AddOnlinePlayer(string username);

        public void RemoveOnlinePlayer(Player player);

        public Player? GetOnlinePlayer(string playerId);

        public void SetPlayerIsReady(Player player);
    }
}
