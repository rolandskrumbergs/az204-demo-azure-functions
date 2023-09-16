using Azure.Storage.Queues;

var connectionString = "DefaultEndpointsProtocol=https;AccountName=az204rolandsdemo9876;AccountKey=1/2PDhN32sU3+37YmzKX/9k+FUd7WxP0zDX4kqkIw6/Fld3/4kokfOhp9JAROiEQ9qGydb9fbVCP+AStJcy1Zg==;EndpointSuffix=core.windows.net";
var queueName = "testqueue";

var queue = new QueueClient(connectionString, queueName, new QueueClientOptions
{
    MessageEncoding = QueueMessageEncoding.Base64
});

var message = "Test message";

await queue.SendMessageAsync(message);