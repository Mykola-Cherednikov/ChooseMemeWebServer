using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.MediaExceptions
{
    public class MediaAlreadyExistsException : DetailedException
    {
		public MediaAlreadyExistsException(string path) : base($"Media already exists by file path: {path}")
		{
		}
	}
}
