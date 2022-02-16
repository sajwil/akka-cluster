using Akka.Actor;
using Akka.Event;
using Contract.Messages;

namespace Contract.Actors
{
    public class ApiActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IActorRef _actorReference;

        public ApiActor(IActorRef actorReference)
        {
            _actorReference = actorReference;
            Receive<HelloMessage>(x => GetHelloMessage(x));
            Receive<ApiWorkflowMessage>(x => GetWorkflowMessage(x));
        }

        private void GetWorkflowMessage(ApiWorkflowMessage x)
        {
            _actorReference.Ask<string>(new WorkflowMessage(x.CorrelationId, x.Amount, Sender)).PipeTo(Sender);
        }

        private void GetHelloMessage(HelloMessage message)
        {
            _log.Debug($"Path is {_actorReference.Path}");
            _log.Debug($"CorrelationId is {message.CorrelationId}");
            _actorReference.Ask<string>(new EngineMessage(message.CorrelationId, message.Message, Sender)).PipeTo(Sender);
        }

        public static Props Props(IActorRef actorReference)
        {
            return Akka.Actor.Props.Create(() => new ApiActor(actorReference));
        }
    }
}