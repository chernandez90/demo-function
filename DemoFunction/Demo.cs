using DemoFunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Net;
using DemoFunction.Logic;
using Microsoft.Extensions.Configuration;

namespace DemoFunction
{
    public class Demo
    {
        private readonly ILogger<Demo> _logger;
        private readonly ISampleLogic _logic;
        private readonly IConfiguration _config;

        public Demo(ILoggerFactory loggerFactory, ISampleLogic logic, IConfiguration config)
        {
            _logger = loggerFactory.CreateLogger<Demo>();
            _logic = logic;
            _config = config;
        }

        [Function("Demo")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var data=await JsonSerializer.DeserializeAsync<PersonModel>(req.Body);
            if(data == null)
            {
                _logger.LogError("Invalid request data");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            string conn = _config.GetConnectionString("Default");
            string test = _config.GetValue<string>("Test:Test1");
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(conn + " - " + test);
            return response;
        }
    }
}
