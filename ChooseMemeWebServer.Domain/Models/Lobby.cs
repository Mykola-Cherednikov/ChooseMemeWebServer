using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Models
{
    public class Lobby
    {
        public string Code { get; set; } = string.Empty;

        public List<Player> Players { get; set; } = new List<Player>();

        public Server Server { get; set; } = null!;

        public bool IsAllPlayersReady { get => Players.All(p => p.IsReady); }

        public Preset Preset { get; set; } = null!;

        public LobbyStatus Status { get; set; }

        public Queue<LobbyStatus> StatusQueue { get; set; } = new Queue<LobbyStatus>();
    }

    public enum LobbyStatus
    {
        WaitingForPlayers,
        GameStart,
        AskQuestion,
        AnswerQuestion,
        QuestionResults,
        EndResult,
        End
    }
}
