// See https://aka.ms/new-console-template for more information

using AZ204Demo;
using Azure.Storage.Queues;
using System.Text.Json;

const int NumberOfMessagesToGenerate = 1000;

string connectionString = "";

var queueClient = new QueueClient(connectionString, "orders", new QueueClientOptions
{
    MessageEncoding = QueueMessageEncoding.Base64
});

await queueClient.CreateIfNotExistsAsync();

for (int i = 0; i < NumberOfMessagesToGenerate; i++)
{
    var order = new Order
    {
        id = Guid.NewGuid(),
        Created = DateTime.Now,
        Products = new List<OrderedProduct>
        {
            new OrderedProduct
            {
                Amount = i,
                Description = "Some description",
                SKU = $"{Guid.NewGuid()}",
                Title = "Some product",
                Unit = "pcs"
            }
        },
        DeliveryAddress = new Address
        {
            City = "Test city",
            Country = "Test country",
            Line = "Test line",
            PostalCode = "XXXXXXX"
        },
        Status = OrderStatus.Created
    }; 
    var message = JsonSerializer.Serialize(order);
    await queueClient.SendMessageAsync(message);
    Console.WriteLine($"Message {i} sent");
}
