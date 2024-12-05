using MediatR;

namespace ChooseMemeWebServer.Core.Commands.AdministrationCommands.GetLobbies
{
    public class GetLobbiesCommand : IRequest<GetLobbiesResponse>
    {
    }
}
