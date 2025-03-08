using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.LobbyExceptions
{
    public class ServerLobbyOwnerException : ExpectedException
    {
		public ServerLobbyOwnerException() : base("Server is not owner of this lobby")
		{
		}
	}
}
