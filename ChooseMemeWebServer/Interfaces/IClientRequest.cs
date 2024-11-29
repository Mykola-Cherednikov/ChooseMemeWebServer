using ChooseMemeWebServer.Models;
using MediatR;

namespace ChooseMemeWebServer.Interfaces
{
    public interface IClientRequest : IRequest
    {
        public Lobby Lobby { get; set; }

        public Player Player { get; set; }
    }
}
