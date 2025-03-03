using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.PlayerExceptions
{
    public class PlayerNotFoundException : DetailedException
    {
		public PlayerNotFoundException() : base("Player not found")
		{
		}
	}
}
