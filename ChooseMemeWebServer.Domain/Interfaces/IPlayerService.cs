using ChooseMemeWebServer.Application.DTO.PlayerService;
using ChooseMemeWebServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void SetPlayerIsReady(SetPlayerIsReadyDTO data);
    }
}
