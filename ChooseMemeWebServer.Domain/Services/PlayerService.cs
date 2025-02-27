using AutoMapper;
using ChooseMemeWebServer.Application.Common.WebSocket;
using ChooseMemeWebServer.Application.DTO;
using ChooseMemeWebServer.Application.DTO.PlayerService.Request;
using ChooseMemeWebServer.Application.DTO.PlayerService.Response;
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
            Player? resultPlayer = null!;

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
        public async Task SetPlayerIsReady(SetPlayerIsReadyRequestDTO data)
        {
            if (data.Player.IsReady || data.Lobby.Status != LobbyStatus.WaitingForPlayers)
            {
                return;
            }

            data.Player.IsReady = true;

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnPlayerIsReady, mapper.Map<PlayerDTO>(data.Player));
            await sender.SendMessageToServer(data.Lobby, payload);
            await sender.SendMessageToPlayer(data.Player, payload);
        }

        public async Task SetLeader(Player player, Lobby lobby)
        {
            player.IsLeader = true;

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.NewLeader, mapper.Map<PlayerDTO>(player));

            await sender.SendMessageToServer(lobby, payload);
            await sender.SendMessageToPlayer(player, payload);
        }

        public async Task ChooseMedia(ChooseMediaRequestDTO data)
        {
            var chosenMedia = data.Player.Media.FirstOrDefault(m => m.Id == data.MediaId);

            if (chosenMedia == null)
            {
                return;
            }

            data.Player.IsReady = true;
            data.Player.ChosenMedia = chosenMedia;

            var payload = new WebSocketResponseMessage(WebSocketMessageResponseType.OnChooseMedia, new ChooseMediaResponseDTO() { PlayerId = data.Player.Id, MediaId = data.Player.ChosenMedia.Id });

            await sender.SendMessageToServer(data.Lobby, payload);
            await sender.SendMessageToPlayer(data.Player, payload);
        }
    }
}
