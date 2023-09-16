using Azure.Storage.Queues;

var connectionString = "DefaultEndpointsProtocol=https;AccountName=az204rolandsdemo9876;AccountKey=1/2PDhN32sU3+37YmzKX/9k+FUd7WxP0zDX4kqkIw6/Fld3/4kokfOhp9JAROiEQ9qGydb9fbVCP+AStJcy1Zg==;EndpointSuffix=core.windows.net";
var queueName = "testqueue";

var queue = new QueueClient(connectionString, queueName, new QueueClientOptions
{
    MessageEncoding = QueueMessageEncoding.Base64
});

var properties = queue.GetProperties();

var cachedMessagesCount = properties.Value.ApproximateMessagesCount;

Console.WriteLine($"There are approximately {cachedMessagesCount} messages in the queue {queueName}");

Console.WriteLine("Peeking top 10 messages...");
var peekedMessages = await queue.PeekMessagesAsync(maxMessages: 10);

foreach (var message in peekedMessages.Value)
{
    Console.WriteLine($"Peeked message: {message.MessageText}");
}

Console.WriteLine("Peeking top 10 messages...");
var receivedMessages = await queue.ReceiveMessagesAsync(maxMessages: 10);

foreach (var message in receivedMessages.Value)
{
    Console.WriteLine($"Received message: {message.MessageText}");
}

