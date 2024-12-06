using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.GetPlayers
{
    public class GetPlayersCommand : IRequest<GetPlayersResponse>
    {
    }
}
