using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Interfaces
{
    public interface IPlayerRequest : IRequest
    {
        public Lobby Lobby { get; set; }

        public Player Player { get; set; }
    }

    public interface IClientRequest<out TResponse> : IRequest
    {
        public Lobby Lobby { get; set; }

        public Player Player { get; set; }
    }
}
