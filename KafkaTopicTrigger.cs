// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

namespace DaprTopicTriggerKafka
{
    using System.Text.Json;
    using CloudNative.CloudEvents;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Dapr;
    using Microsoft.Extensions.Logging;

    public static class KafkaTopicTrigger
    {
        /// <summary>
        /// Visit https://aka.ms/azure-functions-dapr to learn how to use the Dapr extension.
        /// These tasks should be completed prior to running :
        ///   1. Install Dapr
        ///   2. Change the bundle name in host.json to "Microsoft.Azure.Functions.ExtensionBundle.Preview" and the version to "[4.*, 5.0.0)"
        /// Start function app with Dapr: dapr run --app-id functionapp --app-port 3001 --dapr-http-port 3501 --resources-path .\components\ -- func host start
        /// Invoke function app: dapr publish --pubsub pubsub --publish-app-id functionapp --topic A --data '{\"value\": { \"orderId\": \"42\" } }'
        /// </summary>
        /// <param name="subEvent">Cloud event sent by Dapr runtime.</param>
        /// <param name="value">Value will be saved against the given key in state store.</param>
        /// <param name="log">Function logger.</param>
        [FunctionName("KafkaTopicTrigger")]
        public static void Run(
            [DaprTopicTrigger("pubsub", Topic = "A")] CloudEvent subEvent,
            ILogger log)
        {
            log.LogInformation("C# DaprTopic trigger with DaprState output binding function processed a request from the Dapr Runtime.");

            log.LogInformation($"Event Data: {JsonSerializer.Serialize(subEvent.Data)}");
        }
    }
}