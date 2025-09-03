using Confluent.Kafka;

namespace KafkaWebAPI.Services
{
    public class KafkaConsumerService
    {
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly string _groupId = "kafka-webapi-consumer";

        public async Task<List<string>> ConsumeAsync(string topic, int messageCount = 5)
        {
            var messages = new List<string>();

            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = _groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(topic);

            try
            {
                for (int i = 0; i < messageCount; i++)
                {
                    var cr = consumer.Consume(TimeSpan.FromSeconds(5));
                    if (cr != null)
                    {
                        messages.Add(cr.Message.Value);
                    }
                }
            }
            finally
            {
                consumer.Close();
            }

            return messages;
        }
    }
}
