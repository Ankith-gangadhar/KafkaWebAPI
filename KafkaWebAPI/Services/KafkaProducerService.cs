using Confluent.Kafka;

namespace KafkaWebAPI.Services
{
    public class KafkaProducerService
    {
        private readonly string _bootstrapServers = "localhost:9092";

        public async Task ProduceAsync(string topic, string message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            var dr = await producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = message
            });

            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
    }
}
