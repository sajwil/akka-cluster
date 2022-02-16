using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using Contract.Messages;

namespace Contract.Actors
{
    public class WorkflowActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private IDictionary<Guid, decimal> _worflowCollection;
        public WorkflowActor()
        {
            _worflowCollection = new Dictionary<Guid, decimal>();
            Receive<WorkflowMessage>(x => Calculate(x));
        }

        private void Calculate(WorkflowMessage x)
        {
            _log.Debug($"Path is {Self.Path}");
            if (!_worflowCollection.ContainsKey(x.CorrelationId))
            {
                _worflowCollection[x.CorrelationId] = 0;
            }

            _worflowCollection[x.CorrelationId] += x.Amount;
            var currentAmount = _worflowCollection[x.CorrelationId];
            _log.Debug($"Current Value is {currentAmount}");
            x.Sender.Tell(currentAmount);
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new WorkflowActor());
        }
    }
}