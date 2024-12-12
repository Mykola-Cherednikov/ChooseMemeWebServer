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

            onlinePlayers.Add(bot.Id, bot);

            return bot;
        }

        public Player? GetOnlinePlayer(string playerId)
        {
            onlinePlayers.TryGetValue(playerId, out Player? player);

            return player;
        }

        public List<Player> GetOnlinePlayers()
        {
            return onlinePlayers.Values.ToList();
        }

        public void RemoveOnlinePlayer(Player player)
        {
            onlinePlayers.Remove(player.Id);
        }

        public void SetPlayerIsReady(Player player)
        {
            player.IsReady = !player.IsReady;

            var payload = new WebSocketData() { CommandTypeName = "PlayerIsReady", Data = JsonSerializer.Serialize(_mapper.Map<PlayerDTO>(player)) };
            _sender.SendMessageToServer(player.Lobby, payload);
        }
    }
}
