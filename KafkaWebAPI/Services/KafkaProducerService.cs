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
                var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                return $"Delivered '{message}' to {deliveryResult.TopicPartitionOffset}";
            }
            catch (ProduceException<Null, string> ex)
            {
                return $"Kafka produce error: {ex.Error.Reason}";
            }
        }
    }
}
