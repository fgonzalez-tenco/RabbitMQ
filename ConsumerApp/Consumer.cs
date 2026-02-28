using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Simone.Common.RabbitMQ.Abstractions;

namespace ConsumerApp
{
    /// <summary>
    /// The Consumer class is responsible for processing messages received from a RabbitMQ queue. It implements the IConsumer interface, which defines a method for processing messages. The Consumer class uses dependency injection to
    /// receive an IServiceScopeFactory and an ILoggerFactory instance. The Process method is called when a message is received, and it logs the content of the message along with its CorrelationId. The method returns a tuple indicating whether the message should be acknowledged and an optional reason for any failure.
    /// </summary>
    public class Consumer : IConsumer<Message>
    {
        /// <summary>
        /// The ILogger instance is used for logging information about the messages being processed. It logs the CorrelationId and the content of the message when a message is received. This helps in tracking the flow of messages through the system and diagnosing any issues that may arise during message processing.
        /// </summary>
        private readonly ILogger<Consumer> _logger;

        /// <summary>
        /// The constructor of the Consumer class initializes the logger instance using dependency injection. It receives an IServiceScopeFactory and an ILoggerFactory, and it creates a logger for the Consumer class. This allows the Consumer to log information about the messages it processes.
        /// </summary>
        public Consumer(IServiceScopeFactory scopeFactory, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Consumer>();
        }

        /// <summary>
        /// The Process method is called when a message is received from the RabbitMQ queue. It logs the CorrelationId and the content of the message using the ILogger instance. The method returns a
        /// tuple indicating that the message should be acknowledged (true) and that there is no reason for failure (null). This means that the message has been successfully processed and can be removed from the queue.
        /// </summary>
        public async Task<(bool acknowledge, string? reason)> Process(Message message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("[{CorrelationId}] Mensaje recibido: {Message}", message.CorrelationId, message);
            return (true, null);
        }
    }
}