using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.ServerExceptions
{
    public class ServerNotFoundException : ExpectedException
    {
		public ServerNotFoundException() : base("Server not found")
		{
		}
	}
}
