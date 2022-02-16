using System;
using Akka.Actor;
using Akka.Routing;

namespace Contract.Messages
{
    public class EngineMessage : IConsistentHashable
    {
        public string Message { get; }
        public IActorRef Sender { get; }
        public Guid CorrelationId { get; }
        public object ConsistentHashKey { get { return Message; } }

        public EngineMessage(Guid correlationId, string message, IActorRef sender)
        {
            CorrelationId = correlationId;
            Message = message;
            Sender = sender;
        }
    }
}