using ChooseMemeWebServer.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Commands.UnauthorizedCommands.ServerJoinLobby
{
    public class ServerJoinLobbyHandler : IRequestHandler<ServerJoinLobbyCommand>
    {
        private readonly ILobbyService _lobbyService;

        public ServerJoinLobbyHandler(ILobbyService lobbyService) { 
            _lobbyService = lobbyService;
        }

        public async Task Handle(ServerJoinLobbyCommand request, CancellationToken cancellationToken)
        {
            await _lobbyService.ServerJoinToLobby(request.Lobby);
        }
    }
}
