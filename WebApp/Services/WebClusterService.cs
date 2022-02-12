using Akka.Actor;
using Akka.Cluster;
using Contract.Services;

namespace WebApp.Services
{
    public class WebClusterService
    {
        private ActorSystem _clusterNode;
        private readonly IClusterBuilder _clusterBuilder;

        public WebClusterService(IClusterBuilder clusterBuilder)
        {
            _clusterBuilder = clusterBuilder;
        }

        public void StartClusterNode()
        {
            _clusterBuilder.StartWebClusterNode();
            _clusterNode = _clusterBuilder.GetClusterNode();
        }

        public void StopClusterNode()
        {
            var cluster = Cluster.Get(_clusterNode);
            cluster.RegisterOnMemberRemoved(MemberRemoved);
            cluster.Leave(cluster.SelfAddress);
        }

        private async void MemberRemoved()
        {
            await _clusterNode.Terminate();
        }
    }
}