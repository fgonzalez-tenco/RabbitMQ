using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Simone.Common.RabbitMQ.Services;
using Simone.Common.RabbitMQ.Models;

namespace ProducerApp
{
    public class Publisher : BasePublisherService
    {
        /// <summary>
        /// The Publisher class is responsible for sending messages to a RabbitMQ exchange. It inherits from the BasePublisherService, which provides common functionality for publishing messages. The Publisher class uses dependency injection to receive an IServiceScopeFactory, an ILoggerFactory, and an IConfiguration instance. The SendAsync method creates a new message with a unique CorrelationId and sends it to the specified exchange. It also logs the result of the send operation, including any failures or successful sends.
        /// </summary>
        private readonly ILogger<Publisher> _logger;
        /// <summary>
        /// The IConfiguration instance is used to access configuration settings, such as the RabbitMQ exchange name.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The constructor of the Publisher class initializes the logger and configuration instances using dependency injection. It also calls the base constructor of the BasePublisherService to set up the necessary services for publishing messages.
        /// </summary> 
        public Publisher(IServiceScopeFactory scopeFactory, ILoggerFactory loggerFactory, IConfiguration configuration)
            : base(scopeFactory, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Publisher>();
            _configuration = configuration;
        }

        /// <summary>
        /// The ProcessName property is overridden to return the name of the process, which in this case is "Publisher". This can be used for logging and tracking purposes to identify which component is sending messages.
        /// </summary>
        public override string ProcessName => nameof(Publisher);

        /// <summary>
        /// The SendAsync method is responsible for creating a new message and sending it to the RabbitMQ exchange. It generates a unique CorrelationId for the message, sets the Data property, and then calls the base Send method to send the message to the specified exchange. The method also logs the result of the send operation, including any failures or successful sends.
        /// </summary>
        public async Task SendAsync()
        {
            var exchange = _configuration.GetValue<string>("RabbitMQ:DefaultExchange");
            var message = new Message()
            {
                CorrelationId = CorrelationId.NewId(),
                Data = "Hello, RabbitMQ!"
            };

            SendMessageResults result = await base.Send(message, exchange);

            if (result != SendMessageResults.Ok)
                _logger.LogError("[{CorrelationId}] Failure sending message: {Message} to the exchange {Exchange}. Result: {Result}",
                    message.CorrelationId, message.Data, exchange, result);
            else
                _logger.LogInformation("[{CorrelationId}] Message {Message} sent to the exchange {Exchange}.",
                    message.CorrelationId, message.Data, exchange);
        }
    }
}