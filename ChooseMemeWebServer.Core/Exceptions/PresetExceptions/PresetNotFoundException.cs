using ChooseMemeWebServer.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMemeWebServer.Core.Exceptions.PresetExceptions
{
    public class PresetNotFoundException : DetailedException
    {
		public PresetNotFoundException() : base("Preset not found")
		{
		}
	}
}
