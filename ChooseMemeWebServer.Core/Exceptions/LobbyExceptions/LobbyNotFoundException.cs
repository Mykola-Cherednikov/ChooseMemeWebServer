using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Application.Exceptions
{
    public class LobbyNotFoundException : ExpectedException
    {
		public LobbyNotFoundException(string code) : base($"Lobby not found by code: {code}")
		{
		}
	}
}
