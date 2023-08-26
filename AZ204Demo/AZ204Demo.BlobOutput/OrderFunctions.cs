using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AZ204Demo.BlobOutput
{
    public class OrderFunctions
    {
        private readonly ILogger _logger;

        public OrderFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<OrderFunctions>();
        }

        [Function("ReceiveOrder")]
        public async Task<MultiResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
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
                Order = JsonSerializer.Serialize(order),
                HttpResponse = response
            };
        }
    }

    public class MultiResponse
    {
        [BlobOutput("orders/order-{DateTime}", Connection = "OrdersStorage")]
        public required string Order { get; set; }
        public required HttpResponseData HttpResponse { get; set; }
    }
}
