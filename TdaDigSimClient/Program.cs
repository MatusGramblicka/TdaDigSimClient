using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TdaDigSimClient.HttpRepository;

namespace TdaDigSimClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddHttpClient("SimulatorAPI", cl =>
            //{
            //    cl.BaseAddress = new Uri("");
            //});
            builder.Services.AddHttpClient("SimulatorAPI");

            builder.Services.AddScoped(
                sp => sp.GetService<IHttpClientFactory>().CreateClient("SimulatorAPI"));

            builder.Services.AddScoped<ISimulatorHttpRepository, SimulatorHttpRepository>();

            await builder.Build().RunAsync();
        }
    }
}
