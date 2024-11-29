using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;

namespace ChooseMemeWebServer.Core.Commands
{
    public abstract class BaseCommand : IClientRequest
    {
        public Lobby Lobby { get; set; } = null!;

        public Player Player { get; set; } = null!;
    }
}
