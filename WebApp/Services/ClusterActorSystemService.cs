using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;
using Contract.Actors;
using Contract.Services;

namespace WebApp.Services
{
    public interface IClusterActorSystemService
    {
        Task<T> GetAnswerFromCluster<T>(object message);
        void TellCluster(object message);
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

        public Task<T> GetAnswerFromCluster<T>(object message)
        {
            return _apiActor.Ask<T>(message);
        }

        public void TellCluster(object message)
        {
            _apiActor.Tell(message);
        }
    }
}