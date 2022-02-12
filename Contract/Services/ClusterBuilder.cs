using System;
using System.IO;
using Akka.Actor;
using Akka.Bootstrap.Docker;
using Akka.Cluster;
using Akka.Configuration;
using Akka.DependencyInjection;
using Contract.Constants;

namespace Contract.Services
{
    public interface IClusterBuilder
    {
        ActorSystem GetClusterNode();
        void StartEngineClusterNode();
        void StartWebClusterNode();
        void StopClusterNode();
    }

    public class ClusterBuilder : IClusterBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        private ActorSystem clusterActorSystem;

        public ActorSystem GetClusterNode() => clusterActorSystem;

        public ClusterBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void StartEngineClusterNode()
        {
            StartClusterNode(ConfigurationFactory.ParseString(File.ReadAllText("engine.conf")));
        }

        public void StartWebClusterNode()
        {
            StartClusterNode(ConfigurationFactory.ParseString(File.ReadAllText("web.conf")));
        }

        private ActorSystem StartClusterNode(Config configuration)
        {
            var config = configuration.BootstrapFromDocker();
            var bootstrap = BootstrapSetup.Create()
               .WithConfig(config)
               .WithActorRefProvider(ProviderSelection.Cluster.Instance);

            var diSetup = DependencyResolverSetup.Create(_serviceProvider);
            var actorSystemSetup = bootstrap.And(diSetup);

            clusterActorSystem = ActorSystem.Create(ClusterConstant.CLUSTER_NAME, actorSystemSetup);

            return clusterActorSystem;
        }

        public void StopClusterNode()
        {
            var cluster = Cluster.Get(clusterActorSystem);
            cluster.RegisterOnMemberRemoved(MemberRemoved);
            cluster.Leave(cluster.SelfAddress);
        }

        private async void MemberRemoved()
        {
            await clusterActorSystem.Terminate();
        }
    }
}
