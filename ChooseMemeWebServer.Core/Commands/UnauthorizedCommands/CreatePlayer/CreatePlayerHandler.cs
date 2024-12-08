using ChooseMemeWebServer.Core.Interfaces;
using MediatR;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.CreatePlayer
{
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
}
