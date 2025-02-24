using ChooseMemeWebServer.Application.DTO.PlayerService;
using ChooseMemeWebServer.Application.Models;

namespace ChooseMemeWebServer.Application.Interfaces
{
    public interface IPlayerService
    {
        public List<Player> GetOnlinePlayers();

        public List<Player> GetOnlineBots();

        public Player AddOnlinePlayer(string username);

        public Player AddBot();

        public void RemoveOnlinePlayer(Player player);

        public Player? GetOnlinePlayer(string playerId);

        public Task SetPlayerIsReady(SetPlayerIsReadyDTO data);

        public Task SetLeader(Player player, Lobby lobby);
    }
}
