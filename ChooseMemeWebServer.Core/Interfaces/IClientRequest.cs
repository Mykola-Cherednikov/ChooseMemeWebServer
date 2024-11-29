using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface IClientRequest : IRequest
    {
        public Lobby Lobby { get; set; }

        public Player Player { get; set; }
    }
}
