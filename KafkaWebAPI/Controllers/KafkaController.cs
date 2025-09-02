using KafkaWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KafkaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly KafkaProducerService _producerService;

        public KafkaController(KafkaProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpPost("produce")]
        public async Task<IActionResult> ProduceMessage([FromQuery] string message)
        {
            var result = await _producerService.ProduceAsync("test-topic", message);
            return Ok(result);
        }
    }
}
