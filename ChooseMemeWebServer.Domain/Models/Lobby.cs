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

        public Preset Preset { get; set; } = null!;

        public List<Media> UsedMedia { get; set; } = new();

        public List<Question> UsedQuestions { get; set; } = new();
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
