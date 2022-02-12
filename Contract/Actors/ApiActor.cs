﻿using Akka.Actor;
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
        }

        private void GetHelloMessage(HelloMessage message)
        {
            _log.Debug($"Path is {_actorReference.Path}");
            _actorReference.Ask<string>(new EngineMessage(message.CorrelationId, message.Message, Sender)).PipeTo(Sender);
        }

        public static Props Props(IActorRef actorReference)
        {
            return Akka.Actor.Props.Create(() => new ApiActor(actorReference));
        }
    }
}