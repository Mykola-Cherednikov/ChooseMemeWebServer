using ChooseMemeWebServer.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Commands.PlayerCommands
{
    public class PlayerLeaveCommand : PlayerBaseCommand
    {
    }

    public class PlayerLeaveHandler : IRequestHandler<PlayerLeaveCommand>
    {
        private readonly ILobbyService _lobbyService;

        public PlayerLeaveHandler(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        public async Task Handle(PlayerLeaveCommand request, CancellationToken cancellationToken)
        {
            await _lobbyService.LeaveFromLobby(request.Player);
        }
    }
}
