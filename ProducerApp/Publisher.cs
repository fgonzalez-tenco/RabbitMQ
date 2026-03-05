using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Simone.Common.RabbitMQ.Models;
using Simone.Common.RabbitMQ.Services;

namespace ProducerApp
{
    public class Publisher(IServiceScopeFactory scopeFactory, ILoggerFactory loggerFactory) : BasePublisherService(scopeFactory, loggerFactory)
    {
        /// <summary>
        /// The ProcessName property is overridden to return the name of the process, which in this case is "Publisher". This can be used for logging and tracking purposes to identify which component is sending messages.
        /// </summary>
        public override string ProcessName => nameof(Publisher);
        /// <summary>
        /// The ServiceKey constant is defined to provide a unique identifier for the Publisher service. This can be used when registering the service in the dependency injection container or when retrieving it from the container. It helps to ensure that the correct instance of the Publisher service is used when needed.
        /// </summary>
        public const string ServiceKey = nameof(Publisher);

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

            return await Send(message);
        }
    }
}