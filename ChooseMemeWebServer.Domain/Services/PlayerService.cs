using AutoMapper;
using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.PlayerService;
using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Application.Models;
using RandomNameGeneratorLibrary;

namespace ChooseMemeWebServer.Application.Services
{
    public class PlayerService(IWebSocketSenderService sender, IMapper mapper) : IPlayerService
    {
        private static readonly Dictionary<string, Player> onlinePlayers = new Dictionary<string, Player>();
        private static readonly Dictionary<string, Player> onlineBots = new Dictionary<string, Player>();

        public Player AddOnlinePlayer(string username)
        {
            Player player = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Username = username
            };

            onlinePlayers.Add(player.Id, player);

            return player;
        }

        public Player AddBot()
        {
            var nameGenerator = new PersonNameGenerator();
            var name = nameGenerator.GenerateRandomFirstName();

            Player bot = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Username = "Bot " + name,
                IsBot = true
            };

            onlineBots.Add(bot.Id, bot);

            return bot;
        }

        public Player? GetOnlinePlayer(string playerId)
        {
            Player? resultPlayer = new Player();

            if (onlinePlayers.TryGetValue(playerId, out Player? player))
            {
                resultPlayer = player;
            }

            if (onlineBots.TryGetValue(playerId, out Player? bot))
            {
                resultPlayer = bot;
            }

            return resultPlayer;
        }

        public List<Player> GetOnlinePlayers()
        {
            return onlinePlayers.Values.ToList();
        }

        public List<Player> GetOnlineBots()
        {
            return onlineBots.Values.ToList();
        }

        public void RemoveOnlinePlayer(Player player)
        {
            if (onlinePlayers.ContainsKey(player.Id))
            {
                onlinePlayers.Remove(player.Id);
            }

            if (onlineBots.ContainsKey(player.Id))
            {
                onlineBots.Remove(player.Id);
            }
        }

        //WebSocket
        public void SetPlayerIsReady(SetPlayerIsReadyDTO data)
        {
            if (data.Player.IsReady)
            {
                return;
            }

            data.Player.IsReady = true;

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnPlayerIsReady, mapper.Map<PlayerDTO>(data.Player));
            sender.SendMessageToServer(data.Lobby, payload);
            sender.SendMessageToPlayer(data.Player, payload);
        }
    }
}
