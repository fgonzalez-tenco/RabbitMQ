using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Simone.Common.RabbitMQ.Models;
using Simone.Common.RabbitMQ.Services;

namespace ProducerApp
{
    public class Publisher(IServiceScopeFactory scopeFactory, ILoggerFactory loggerFactory) : BasePublisherService(scopeFactory, loggerFactory)
    {
        /// <summary>
        /// The Publisher class is responsible for sending messages to a RabbitMQ exchange. It inherits from the BasePublisherService, which provides common functionality for publishing messages. The Publisher class uses dependency injection to receive an IServiceScopeFactory, an ILoggerFactory, and an IConfiguration instance. The SendAsync method creates a new message with a unique CorrelationId and sends it to the specified exchange. It also logs the result of the send operation, including any failures or successful sends.
        /// </summary>
        private readonly ILogger<Publisher> _logger = loggerFactory.CreateLogger<Publisher>();
        /// <summary>
        /// The ProcessName property is overridden to return the name of the process, which in this case is "Publisher". This can be used for logging and tracking purposes to identify which component is sending messages.
        /// </summary>
        public override string ProcessName => nameof(Publisher);

        /// <summary>
        /// The SendAsync method is responsible for creating a new message and sending it to the RabbitMQ exchange. It generates a unique CorrelationId for the message, sets the Data property, and then calls the base Send method to send the message to the specified exchange. The method also logs the result of the send operation, including any failures or successful sends.
        /// </summary>
        public async Task<SendMessageResults> SendAsync()
        {            
            var message = new Message()
            {
                CorrelationId = CorrelationId.NewId(),
                Data = "Hello, RabbitMQ!"
            };

            SendMessageResults result = await Send(message);

            if (result != SendMessageResults.Ok)
                _logger.LogError("[{CorrelationId}] Failure sending message: {Message}. Result: {Result}",
                    message.CorrelationId, message.Data, result);
            else
                _logger.LogInformation("[{CorrelationId}] Message {Message} sent successfully.",
                    message.CorrelationId, message.Data);
            return result;
        }
    }
}