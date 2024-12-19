using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DemoFunction
{
    public class Demo
    {
        private readonly ILogger<Demo> _logger;

        public Demo(ILogger<Demo> logger)
        {
            _logger = logger;
        }

        [Function("Demo")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteStringAsync("Hello World!");
            return response;
        }
    }
}
