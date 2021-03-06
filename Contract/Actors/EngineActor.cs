using Akka.Actor;
using Akka.Event;
using Contract.Messages;

namespace Contract.Actors
{
    public class EngineActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private IActorRef _workflowActor;

        public EngineActor()
        {
            _workflowActor = Context.ActorOf(WorkflowActor.Props(), "worflow");
            Receive<EngineMessage>(x => GetEngineMessage(x));
            Receive<WorkflowMessage>(x =>
            {
                _workflowActor.Tell(x);
            });
        }

        private void GetEngineMessage(EngineMessage message)
        {
            _log.Debug($"Path is {Self.Path.Address}");
            _log.Debug($"Received message to engine {message.Message} with id {message.CorrelationId}");
            message.Sender.Tell($"Message Recieved to engine with id {message.CorrelationId}");
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new EngineActor());
        }
    }
}