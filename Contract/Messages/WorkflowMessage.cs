using System;
using Akka.Actor;
using Akka.Routing;

namespace Contract.Messages
{
    public class WorkflowMessage : IConsistentHashable
    {
        public IActorRef Sender { get; }
        public Guid CorrelationId { get; }
        public decimal Amount { get; }
        public object ConsistentHashKey => CorrelationId;

        public WorkflowMessage(Guid correlationId, decimal amount, IActorRef sender)
        {
            CorrelationId = correlationId;
            Amount = amount;
            Sender = sender;
        }
    }

    public class ApiWorkflowMessage
    {
        public Guid CorrelationId { get; }
        public decimal Amount { get; }

        public ApiWorkflowMessage(Guid correlationId, decimal amount)
        {
            CorrelationId = correlationId;
            Amount = amount;
        }
    }
}