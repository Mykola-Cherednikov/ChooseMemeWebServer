using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class UsernameIsNullOrEmptyException : ExpectedException
    {
        public UsernameIsNullOrEmptyException() : base("Username is null or empty")
		{
		}
    }
}
