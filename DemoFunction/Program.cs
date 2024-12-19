using DemoFunction.Logic;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration(c =>
    {
        c.AddJsonFile("appsettings.json", optional: true);
        c.AddJsonFile("local.settings.json",optional: true);
        c.AddEnvironmentVariables();
    })
    .ConfigureServices(s =>
    {
        s.AddSingleton<ISampleLogic, SampleLogic>();
    })
    .Build();

host.Run();