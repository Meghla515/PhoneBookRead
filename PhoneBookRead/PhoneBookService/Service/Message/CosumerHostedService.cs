using Cassandra;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBookReadService.Service.Message
{
    public class CosumerHostedService : IHostedService
    {
        private IConsumer<Null, string> consumer;
        private readonly ILogger<CosumerHostedService> _logger;

        public CosumerHostedService(ILogger<CosumerHostedService> logger)
        {
            _logger = logger;
            var consumerConfig = new ConsumerConfig
            {
                GroupId = "phonebook-read",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
                //EnableAutoCommit = true
            };

            consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            consumer.Subscribe("phonebook-incoming");
            var consumeResult = consumer.Consume();
            //_logger.LogInformation("received " + consumeResult.Message.Value);

            while (!cancellationToken.IsCancellationRequested)
            {
                new MessageConsumer().HandlePhonebookMessage(consumeResult.Message.Value);
                await Task.Delay(1000, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            consumer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
