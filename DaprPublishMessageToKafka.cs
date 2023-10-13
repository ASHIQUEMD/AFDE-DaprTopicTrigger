namespace DaprTopicTriggerKafka
{
    using Microsoft.Azure.Functions.Extensions.Dapr.Core;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Dapr;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using System.Net.Http;

    public static class DaprPublishMessageToKafka
    {
        /// <summary>
        /// Sample to use Dapr Topic Trigger and Dapr Publish Output Binding to subscribe to a message bus 
        /// and then republish it to another topic with edited message content
        /// </summary>
        [FunctionName("DaprPublishMessageToKafka")]
        public static void Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DaprPublish(PubSubName = "pubsub", Topic = "A")] out DaprPubSubEvent pubEvent,
            ILogger log)
        {
            log.LogInformation("C# function processed a DaprPublishMessageToKafka request from the Dapr Runtime.");

            var queryParameters = req.RequestUri.ParseQueryString();
            var readRequestBody = req.Content.ReadAsStringAsync().Result;

            pubEvent = new DaprPubSubEvent("Transfer from Topic A: " + queryParameters + readRequestBody);
        }
    }
}
