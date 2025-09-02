using KafkaWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KafkaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly KafkaProducerService _producer;

        public KafkaController(KafkaProducerService producer)
        {
            _producer = producer;
        }

        [HttpPost("produce")]
        public async Task<IActionResult> Produce([FromQuery] string message)
        {
            if (string.IsNullOrEmpty(message))
                return BadRequest("Message cannot be empty");

            var result = await _producer.ProduceAsync("product-topic", message);
            return Ok(result);
        }
    }

}
