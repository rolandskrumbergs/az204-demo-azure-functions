using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AZ204Demo.Scaling
{
    public class ScalingFunction
    {
        private readonly ILogger<ScalingFunction> _logger;

        public ScalingFunction(ILogger<ScalingFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ScalingFunction))]
        public void Run([QueueTrigger("orders", Connection = "QueueStorage")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
