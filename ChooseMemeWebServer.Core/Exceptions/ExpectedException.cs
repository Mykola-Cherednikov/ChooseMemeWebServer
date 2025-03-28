namespace ChooseMemeWebServer.Application.Exceptions
{
    public class ExpectedException : Exception
    {
        public ExpectedException(string message) : base(message)
        {
        }

        public override string ToString()
        {
            return $"Message: {Message}\n" +
                   $"StackTrace: {StackTrace}";
        }
    }
}
