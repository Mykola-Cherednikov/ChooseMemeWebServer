using AutoMapper;
using ChooseMemeWebServer.Core.Common;
using ChooseMemeWebServer.Core.DTO;
using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using RandomNameGeneratorLibrary;
using System.Text.Json;

namespace ChooseMemeWebServer.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IWebSocketSender _sender;
        private readonly IMapper _mapper;

        private static readonly Dictionary<string, Player> onlinePlayers = new Dictionary<string, Player>();
        private static readonly Dictionary<string, Player> onlineBots = new Dictionary<string, Player>();

        public PlayerService(IWebSocketSender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public Player AddOnlinePlayer(string username)
        {
            Player player = new Player()
            {
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
                Username = "Bot " + name,
                IsBot = true
            };

            onlineBots.Add(bot.Id, bot);

            return bot;
        }

        public Player? GetOnlinePlayer(string playerId)
        {
            Player? resultPlayer = new Player();

            if(onlinePlayers.TryGetValue(playerId, out Player? player))
            {
                resultPlayer = player;
            }

            if(onlineBots.TryGetValue(playerId, out Player? bot))
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

        public void SetPlayerIsReady(Player player)
        {
            player.IsReady = !player.IsReady;

            var payload = new WebSocketData(CommandType.PlayerIsReady, _mapper.Map<PlayerDTO>(player));
            _sender.SendMessageToServer(player.Lobby, payload);
        }
    }
}
