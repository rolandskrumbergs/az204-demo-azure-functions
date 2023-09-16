// name of your Service Bus queue

using Azure.Messaging.ServiceBus;

var serviceBusConnectionString = "";

// DEMO FOR BASIC QUEUE
// ---------------------------------------------------

var queueOrTopic = "testqueue";

var client = new ServiceBusClient(serviceBusConnectionString);
var sender = client.CreateSender(queueOrTopic);

var message = new ServiceBusMessage("This is message content");

try
{
    await sender.SendMessageAsync(message);
}
finally
{
    await sender.DisposeAsync();
    await client.DisposeAsync();
}

// DEMO FOR SESSIONS
// ---------------------------------------------------

//var queueOrTopic = "testqueuewithsessions";

//var client = new ServiceBusClient(serviceBusConnectionString);
//var sender = client.CreateSender(queueOrTopic);

//var message = new ServiceBusMessage("This is message content");

//message.SessionId = Guid.NewGuid().ToString();

//try
//{
//    await sender.SendMessageAsync(message);
//}
//finally
//{
//    await sender.DisposeAsync();
//    await client.DisposeAsync();
//}

// DEMO FOR TOPICS
// ---------------------------------------------------

//var topic = "testtopic";

//var client = new ServiceBusClient(serviceBusConnectionString);

//var sender = client.CreateSender(topic);
//var message = new ServiceBusMessage("This is message content");

//// Update properties for different subscriptions
//message.ApplicationProperties.Add(new KeyValuePair<string, object>("OrderState", "Ready"));

//try
//{
//    await sender.SendMessageAsync(message);
//}
//finally
//{
//    await sender.DisposeAsync();
//    await client.DisposeAsync();
//}