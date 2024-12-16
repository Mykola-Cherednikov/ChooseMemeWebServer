using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Domain.Models
{
    public class Game
    {
        public Lobby Lobby { get; set; } = null!;

        public GameState CurrentGameState { get; set; }

        public GameState NextGameState { get; set; }

        public Dictionary<Player, int> PlayerPoints { get; set; } = null!;
    }

    public enum GameState
    {

    }
}
