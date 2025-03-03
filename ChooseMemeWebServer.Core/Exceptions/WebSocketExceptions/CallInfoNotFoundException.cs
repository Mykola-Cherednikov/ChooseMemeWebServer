using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class CallInfoNotFoundException : DetailedException
    {
		public CallInfoNotFoundException(string type) : base($"Call info not found by type: {type}")
		{
		}
	}
}
