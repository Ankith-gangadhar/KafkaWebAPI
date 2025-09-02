using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KafkaWebAPI.Services
{
    public class KafkaProducerService
    {
        private readonly string _bootstrapServers = "localhost:9092";

        public async Task<string> ProduceAsync(string topic, string message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
            using var producer = new ProducerBuilder<Null, string>(config).Build();

            try
            {
                var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                return $"Message '{message}' delivered to {result.TopicPartitionOffset}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
