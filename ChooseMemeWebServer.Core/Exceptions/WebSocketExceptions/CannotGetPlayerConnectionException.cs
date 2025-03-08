using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class CannotGetPlayerConnectionException : ExpectedException
    {
		public CannotGetPlayerConnectionException(string username) : base($"Cannot get connetion with player: {username}")
		{
		}
	}
}
