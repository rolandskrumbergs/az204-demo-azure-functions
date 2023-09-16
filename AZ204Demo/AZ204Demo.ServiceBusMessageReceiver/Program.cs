// the client that owns the connection and can be used to create senders and receivers
using Azure.Messaging.ServiceBus;

var serviceBusConnectionString = "";

// DEMO FOR BASIC QUEUE
// ---------------------------------------------------

var queue = "testqueue";

var client = new ServiceBusClient(serviceBusConnectionString);
var processor = client.CreateProcessor(queue, new ServiceBusProcessorOptions());

async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    Console.WriteLine($"Received: {body}");

    await args.CompleteMessageAsync(args.Message);
}

Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}

try
{
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;

    await processor.StartProcessingAsync();

    Console.WriteLine("Wait for a minute and then press any key to end the processing");
    Console.ReadKey();

    Console.WriteLine("\nStopping the receiver...");
    await processor.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");
}
finally
{
    await processor.DisposeAsync();
    await client.DisposeAsync();
}

// DEMO FOR SESSIONS
// ---------------------------------------------------

//var queue = "testqueuewithsessions";

//var client = new ServiceBusClient(serviceBusConnectionString);
//var processor = client.CreateProcessor(queue, new ServiceBusProcessorOptions());

//async Task MessageHandler(ProcessMessageEventArgs args)
//{
//    string body = args.Message.Body.ToString();
//    Console.WriteLine($"Received: {body}");

//    await args.CompleteMessageAsync(args.Message);
//}

//Task ErrorHandler(ProcessErrorEventArgs args)
//{
//    Console.WriteLine(args.Exception.ToString());
//    return Task.CompletedTask;
//}

//try
//{
//    processor.ProcessMessageAsync += MessageHandler;
//    processor.ProcessErrorAsync += ErrorHandler;

//    await processor.StartProcessingAsync();

//    Console.WriteLine("Wait for a minute and then press any key to end the processing");
//    Console.ReadKey();

//    Console.WriteLine("\nStopping the receiver...");
//    await processor.StopProcessingAsync();
//    Console.WriteLine("Stopped receiving messages");
//}
//finally
//{
//    await processor.DisposeAsync();
//    await client.DisposeAsync();
//}


// DEMO FOR TOPICS
// ---------------------------------------------------

//var client = new ServiceBusClient(serviceBusConnectionString);

//var topic = "testtopic";
//var subscription = "demo";

//var processor = client.CreateProcessor(topic, subscription, new ServiceBusProcessorOptions());

//try
//{
//    processor.ProcessMessageAsync += MessageHandler;
//    processor.ProcessErrorAsync += ErrorHandler;

//    await processor.StartProcessingAsync();

//    Console.WriteLine("Wait for a minute and then press any key to end the processing");
//    Console.ReadKey();

//    Console.WriteLine("\nStopping the receiver...");
//    await processor.StopProcessingAsync();
//    Console.WriteLine("Stopped receiving messages");
//}
//finally
//{
//    await processor.DisposeAsync();
//    await client.DisposeAsync();
//}

//async Task MessageHandler(ProcessMessageEventArgs args)
//{
//    string body = args.Message.Body.ToString();
//    Console.WriteLine($"Received: {body}");

//    await args.CompleteMessageAsync(args.Message);
//}

//Task ErrorHandler(ProcessErrorEventArgs args)
//{
//    Console.WriteLine(args.Exception.ToString());
//    return Task.CompletedTask;
//}