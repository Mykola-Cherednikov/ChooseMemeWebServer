using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.LobbyExceptions
{
    public class LobbyAlreadyHaveServerException : ExpectedException
    {
		public LobbyAlreadyHaveServerException() : base("Lobby already have server")
		{
		}
	}
}
