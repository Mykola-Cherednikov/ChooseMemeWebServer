using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.PlayerExceptions
{
    public class PlayerIsReadyException : ExpectedException
    {
		public PlayerIsReadyException(string username) : base($"Player '{username}' is ready")
		{
		}
	}
}
