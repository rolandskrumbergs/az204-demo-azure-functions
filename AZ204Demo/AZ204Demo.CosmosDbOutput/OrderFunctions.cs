using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace AZ204Demo.CosmosDbOutput
{
    public class OrderFunctions
    {
        private readonly ILogger _logger;

        public OrderFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<OrderFunctions>();
        }

        [Function("ReceiveOrder")]
        public async Task<MultiResponse> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            using StreamReader streamReader = new(req.Body);
            var requestBody = await streamReader.ReadToEndAsync();

            var order = JsonSerializer.Deserialize<Order>(requestBody);
            order.id = Guid.NewGuid();
            order.Created = DateTime.Now;

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            return new MultiResponse
            {
                Order = order!,
                HttpResponse = response
            };
        }
    }

    public class MultiResponse
    {
        [CosmosDBOutput("OrdersDatabase", "orders",
            Connection = "CosmosDbConnectionString", CreateIfNotExists = false)]
        public required Order Order { get; set; }
        public required HttpResponseData HttpResponse { get; set; }
    }
}
