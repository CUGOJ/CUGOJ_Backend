using CUGOJ.Backend.Silo.Init;
using CUGOJ.Backend.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Hosting;

namespace CUGOJ.Backend.Test
{
    public static partial class Common
    {
        public static string[] CommonConfig { get; set; } = new string[] { };
        public static async Task CommonTest(Func<Task> action, string[]? args = default)
        {
            if (args != default)
                args = CommonConfig.Concat(args).ToArray();
            else
                args = CommonConfig;
            Config.LoadProperties(args, true);

            Init.InitSilo();

            var siloBuilder = new HostBuilder()
                .UseOrleans(builder =>
                {
                    builder.Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = Config.Env;
                        options.ServiceId = "CUGOJ";
                    })
        .UseAdoNetClustering(options =>
                {
                    options.ConnectionString = Config.OrleansDBConnectionString;
                    options.Invariant = "System.Data.SqlClient";
                })
        .ConfigureEndpoints(siloPort: Config.SiloPort, gatewayPort: Config.GatewayPort);
                    // .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(CUGOJ.Backend.Grains.UserGrain).Assembly).WithReferences())
                });
            siloBuilder.ConfigureServices(
                service =>
                {
                    ServiceInit.InitService(service);
                });


            var silo = siloBuilder.Build();
            ServiceInit.LoadSingletonService(silo.Services);

            await silo.StartAsync();
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            await app.StartAsync();

            await action();

            await silo.StopAsync();
        }

    }
}
