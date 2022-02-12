using System.Linq;
using System.Threading.Tasks;
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
            return await _clusterSystem.GetAnswerFromCluster(message);
        }

        [HttpGet("random")]
        public async Task SendRandom()
        {
            foreach (var i in Enumerable.Range(1, 100))
            {
                await _clusterSystem.GetAnswerFromCluster(i.ToString());
            }
        }

        [HttpPost("payload")]
        public async Task SendPayload([FromBody] string payload)
        {

        }
    }
}