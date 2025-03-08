using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.MediaExceptions
{
    public class MediaNotFoundException : ExpectedException
    {
		public MediaNotFoundException() : base("Media not found exception")
		{
		}
	}
}
