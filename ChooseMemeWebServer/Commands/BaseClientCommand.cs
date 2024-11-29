using ChooseMemeWebServer.Interfaces;
using ChooseMemeWebServer.Models;

namespace ChooseMemeWebServer.Commands
{
    public abstract class BaseClientCommand : IClientRequest
    {
        public Lobby Lobby { get; set; } = null!;

        public Player Player { get; set; } = null!;
    }
}
