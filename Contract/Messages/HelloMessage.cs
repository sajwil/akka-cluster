using System;
using Akka.Routing;

namespace Contract.Messages
{
    public class HelloMessage : IConsistentHashable
    {
        public Guid CorrelationId { get; }
        public string Message { get; }
        public object ConsistentHashKey { get { return CorrelationId; } }

        public HelloMessage(Guid correlationId, string message)
        {
            CorrelationId = correlationId;
            Message = message;
        }
    }
}