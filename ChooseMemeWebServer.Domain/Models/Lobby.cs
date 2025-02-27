using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Application.Models
{
    public class Lobby
    {
        public string Code { get; set; } = string.Empty;

        public List<Player> Players { get; set; } = new();

        public Server Server { get; set; } = null!;

        public bool IsAllPlayersReady { get => Players.All(p => p.IsReady); }

        public LobbyStatus Status { get; set; }

        public Queue<LobbyStatus> StatusQueue { get; set; } = new();

        public string PresetId { get; set; } = string.Empty;

        public Queue<Media> Media { get; set; } = new();

        public Queue<Question> Questions { get; set; } = new();
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
