using Simone.Common.RabbitMQ.Models;

namespace ConsumerApp
{
    public class Message: IMessageHeader
    {
        /// <summary>
        /// CorrelationId is a unique identifier for the message, used for tracking and correlation purposes in messaging systems. It helps to trace the flow of messages across different components and services, especially in distributed systems. By assigning a CorrelationId to each message, you can easily identify and correlate related messages, making it easier to debug and monitor the system's behavior.
        /// </summary>
        public string CorrelationId { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// The Type property is a string that represents the type or category of the message. It is used to classify messages and can be helpful for routing, processing, or filtering messages based on their type. In messaging systems, the Type property can be used to determine how a message should be handled or which components should process it. For example, you might have different types of messages for user actions, system events, or notifications, and the Type property can help you distinguish between them.
        /// </summary>
        public string Type => "MessageTest";
        /// <summary>
        /// The Index property is an integer that can be used to represent the position or order of the message in a sequence. It can be helpful for tracking the order of messages, especially when processing a series of related messages. The Index can be used to ensure that messages are processed in the correct order or to identify specific messages within a batch or stream of messages. In some cases, it may also be used for pagination or to indicate the number of times a message has been retried.
        /// </summary>
        public int Index => 0;
        /// <summary>
        /// The Queue property is an optional property that can be used to specify the name of the queue to which the message should be sent or from which it should be received. In messaging systems, queues are used to store and manage messages until they are processed by consumers. By specifying a Queue, you can direct the message to a specific destination or indicate where it should be consumed from. This can be particularly useful in scenarios where you have multiple queues for different types of messages or when you want to implement specific routing logic based on the queue name.
        /// </summary>
        public Queue? Queue { get; set; }
        /// <summary>
        /// The RelatedCorrelationIds property is a collection of tuples that can be used to store related correlation IDs along with their corresponding indices. This can be useful for tracking and correlating messages that are related to each other, especially in scenarios where a message may have multiple related messages or when you want to maintain a history of related messages. Each tuple in the collection contains a CorrelationId and an Index, allowing you to easily identify and correlate related messages based on their correlation IDs and their position in the sequence.
        /// </summary>
        public IEnumerable<(string CorrelationId, int Index)> RelatedCorrelationIds { get; set; } = [];
        /// <summary>
        /// The Data property is a string that represents the actual content or payload of the message. It can contain any relevant information that needs to be transmitted or processed by the messaging system. The Data property is typically used to store the main information that the message is intended to convey, such as user input, system events, or any other relevant data that needs to be communicated between components or services. The content of the Data property can vary widely depending on the specific use case and the requirements of the messaging system.
        /// </summary>
        public string Data { get; set; } = string.Empty;
    }
}