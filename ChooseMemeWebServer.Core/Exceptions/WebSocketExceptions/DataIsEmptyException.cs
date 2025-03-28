using ChooseMemeWebServer.Application.Exceptions;

namespace ChooseMemeWebServer.Core.Exceptions.WebSocketExceptions
{
    public class DataIsEmptyException : ExpectedException
    {
        public DataIsEmptyException() : base("Data is empty")
        {
        }
    }
}
