using System;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AZ204Demo.Events
{
    public class EventHubProcessor
    {
        private readonly ILogger<EventHubProcessor> _logger;

        public EventHubProcessor(ILogger<EventHubProcessor> logger)
        {
            _logger = logger;
        }

        [Function(nameof(EventHubProcessor))]
        public void Run([EventHubTrigger("samples-workitems", Connection = "")] EventData[] events)
        {
            foreach (EventData @event in events)
            {
                _logger.LogInformation("Event Body: {body}", @event.Body);
                _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);
            }
        }
    }
}
