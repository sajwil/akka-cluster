using System;

namespace Contract.Messages
{
    public class HelloMessage
    {
        public Guid CorrelationId { get; }
        public string Message { get; }

        public HelloMessage(Guid correlationId, string message)
        {
            CorrelationId = correlationId;
            Message = message;
        }
    }
}