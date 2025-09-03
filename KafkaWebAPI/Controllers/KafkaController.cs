using KafkaWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly KafkaProducerService _producerService;
        private readonly KafkaConsumerService _consumerService;

        public KafkaController(KafkaProducerService producerService, KafkaConsumerService consumerService)
        {
            _producerService = producerService;
            _consumerService = consumerService;
        }

        [HttpPost("produce")]
        public async Task<IActionResult> Produce([FromQuery] string topic, [FromBody] string message)
        {
            await _producerService.ProduceAsync(topic, message);
            return Ok($"Message sent to topic '{topic}': {message}");
        }

        [HttpGet("consume")]
        public async Task<IActionResult> Consume([FromQuery] string topic, [FromQuery] int count = 5)
        {
            var messages = await _consumerService.ConsumeAsync(topic, count);
            return Ok(messages);
        }
    }
}
