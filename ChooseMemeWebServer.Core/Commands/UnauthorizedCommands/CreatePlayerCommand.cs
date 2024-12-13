using ChooseMemeWebServer.Core.Interfaces;
using ChooseMemeWebServer.Domain.Models;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands
{
    public class CreatePlayerCommand : IRequest<CreatePlayerResponse>
    {
        public string Username { get; set; } = string.Empty;
    }

    public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, CreatePlayerResponse>
    {
        private readonly IPlayerService _playerService;

        public CreatePlayerHandler(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public Task<CreatePlayerResponse> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            CreatePlayerResponse response = new();

            response.Player = _playerService.AddOnlinePlayer(request.Username);

            return Task.FromResult(response);
        }
    }

    public class CreatePlayerResponse
    {
        public Player Player { get; set; } = null!;
    }
}
