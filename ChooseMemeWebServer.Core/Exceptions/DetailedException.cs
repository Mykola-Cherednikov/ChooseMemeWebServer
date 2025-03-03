using System.Runtime.CompilerServices;

namespace ChooseMemeWebServer.Application.Exceptions
{
	public class DetailedException : Exception
	{
		public DetailedException(string message) : base(message)
		{
		}

		public override string ToString()
		{
			return $"Message: {Message}\n" +
				   $"StackTrace: {StackTrace}";
		}
	}
}
