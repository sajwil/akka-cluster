using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;
using Contract.Actors;
using Contract.Messages;
using Contract.Services;

namespace WebApp.Services
{
    public interface IClusterActorSystemService
    {
        Task<string> GetAnswerFromCluster(string message);
    }

    public class ClusterActorSystemService : IClusterActorSystemService
    {
        private readonly IActorRef _apiActor;

        public ClusterActorSystemService(IClusterBuilder clusterBuilder)
        {
            var actorSystem = clusterBuilder.GetClusterNode();
            var router = actorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "api");
            _apiActor = actorSystem.ActorOf(ApiActor.Props(router), "api-actor");
        }

        public Task<string> GetAnswerFromCluster(string message)
        {
            return _apiActor.Ask<string>(new HelloMessage(Guid.NewGuid(), message));
        }
    }
}