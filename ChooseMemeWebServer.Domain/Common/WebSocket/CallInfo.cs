using System.Reflection;

namespace ChooseMemeWebServer.Application.Common.WebSocket
{
    public class CallInfo
    {
        public Type Class { get; } = null!;

        public MethodInfo Method { get; } = null!;

        public Type DataType { get; } = null!;

        private CallInfo()
        {

        }

        public CallInfo(Type serviceType, string methodName, Type dataType)
        {
            Class = serviceType;
            Method = Class.GetMethod(methodName)!;
            DataType = dataType;
        }
    }
}
