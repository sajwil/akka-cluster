using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Contract.Actors;
using Contract.Services;
using Microsoft.Extensions.Hosting;

namespace Engine.Services
{
    public class EngineClusterService : IHostedService
    {
        private ActorSystem _clusterNode;
        private readonly IClusterBuilder _clusterBuilder;
        private readonly IHostApplicationLifetime _appLifetime;

        public EngineClusterService(IClusterBuilder clusterBuilder, IHostApplicationLifetime appLifetime)
        {
            _clusterBuilder = clusterBuilder;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _clusterBuilder.StartEngineClusterNode();
            _clusterNode = _clusterBuilder.GetClusterNode();
            _clusterNode.ActorOf(EngineActor.Props(), "engine");

            _clusterNode.WhenTerminated.ContinueWith(tr =>
            {
                _appLifetime.StopApplication();
            });

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await CoordinatedShutdown.Get(_clusterNode).Run(CoordinatedShutdown.ClrExitReason.Instance);
        }
    }
}