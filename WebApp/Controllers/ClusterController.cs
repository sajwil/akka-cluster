using System;
using System.Linq;
using System.Threading.Tasks;
using Contract.Messages;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClusterController : ControllerBase
    {
        private readonly IClusterActorSystemService _clusterSystem;

        public ClusterController(IClusterActorSystemService clusterSystem)
        {
            _clusterSystem = clusterSystem;
        }

        [HttpGet("message")]
        public async Task<string> SendMessage(string message)
        {
            return await _clusterSystem.GetAnswerFromCluster<string>(new HelloMessage(Guid.NewGuid(), message));
        }

        [HttpGet("random")]
        public void SendRandom()
        {
            foreach (var i in Enumerable.Range(1, 100))
            {
                _clusterSystem.TellCluster(new HelloMessage(Guid.NewGuid(), i.ToString()));
            }
        }
    }
}