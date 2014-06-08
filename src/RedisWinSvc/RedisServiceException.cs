using System;
using System.Runtime.Serialization;

namespace RedisWinSvc
{
    [Serializable]
    public class RedisServiceException : Exception
    {
        public RedisServiceException() { }
        public RedisServiceException(string message) : base(message) { }
        public RedisServiceException(string message, Exception innerException) : base(message, innerException) { }
        protected RedisServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
