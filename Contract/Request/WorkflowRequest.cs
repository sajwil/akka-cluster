using System;

namespace Contract.Request
{
    public class WorkflowRequest
    {
        public Guid CorrelationId { get; set; }
        public decimal Amount { get; set; }
    }
}