using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class InstanceNotFoundException : ExpectedException
    {
		public InstanceNotFoundException(string className) : base($"Instance not found: {className}")
		{
		}
	}
}
