using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Services
{
    public class GameService : IGameService
    {
        private static readonly Dictionary<Lobby, Game> activeGames = new Dictionary<Lobby, Game>();

        public GameService()
        {
            
        }

        public void CreateGame(Lobby lobby)
        {
            
        }

        public void HandleGame()
        {
            throw new NotImplementedException();
        }
    }
}
