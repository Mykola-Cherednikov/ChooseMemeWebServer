using ChooseMemeWebServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface IPlayerService
    {
        public List<Player> GetOnlinePlayers();

        public void AddOnlinePlayer(Player player);

        public void RemoveOnlinePlayer(string id);

        public Player? GetPlayer(string playerId);

        public void SetPlayerIsReady(Player player);
    }
}
