using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Dapr;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace StateOutputIsolatedFunc
{
    public class CreateNewOrder
    {
        [Function("StateOutputBinding")]
        [DaprStateOutput("statestore", Key = "{key}")]
        public static async Task<JsonElement> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "state/{key}")] HttpRequestData req,
            FunctionContext functionContext)
        {
            var log = functionContext.GetLogger("StateOutputBinding");
            log.LogInformation("C# HTTP trigger function processed a request.");

            //using (var reader = new StreamReader(req.Body))
            //{
            //    string requestBody = await reader.ReadToEndAsync();
            //    log.LogInformation($"Requestbody: {requestBody}");
            //    return requestBody;
            //}

            JsonDocument doc = await JsonDocument.ParseAsync(req.Body);
            return doc.RootElement;
        }

        //[Function("RetrieveOrder")]
        //public static void Run(
        //[DaprServiceInvocationTrigger] object args,
        //[DaprStateInput("%StateStoreName%", Key = "order")] JsonElement data, FunctionContext functionContext)
        //{
        //    var log = functionContext.GetLogger("RetrieveOrder");
        //    log.LogInformation("C# function processed a RetrieveOrder request from the Dapr Runtime.");

        //    //print the fetched state value
        //    log.LogInformation(JsonSerializer.Serialize(data));
        //}
    }
}