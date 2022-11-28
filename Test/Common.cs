using CUGOJ.Backend.Silo.Init;
using CUGOJ.Backend.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            Config.InitConfig(args, true);


            var siloBuilder = new HostBuilder()
                .UseOrleans(builder =>
                {

                })
                .UseOrleans(builder =>
                {
                    builder.UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = Config.Env;
                        options.ServiceId = "CUGOJ";
                    })
                    .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                    .Configure<ILoggingBuilder>(logging => logging.AddConsole());
                });
        //        .UseOrleans(builder =>
        //        {
        //            builder.Configure<ClusterOptions>(options =>
        //            {
        //                options.ClusterId = Config.Env;
        //                options.ServiceId = "CUGOJ";
        //            })
        //.UseAdoNetClustering(options =>
        //        {
        //            options.ConnectionString = Config.OrleansDBConnectionString;
        //            options.Invariant = "System.Data.SqlClient";
        //        })
        //.ConfigureEndpoints(siloPort: Config.SiloPort, gatewayPort: Config.GatewayPort);
        //            // .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(CUGOJ.Backend.Grains.UserGrain).Assembly).WithReferences())
        //        });
            siloBuilder.ConfigureServices(
                service =>
                {
                    ServiceInit.InitService(service);
                });


            var silo = siloBuilder.Build();
            ServiceInit.LoadSingletonService(silo.Services);
            await Config.InitRemoteConfig();

            Init.InitSilo();

            await silo.StartAsync();
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            await app.StartAsync();

            await action();

            await silo.StopAsync();
        }

        public static async Task CommonTest(Func<IClusterClient,Task> action, string[]? args = default)
        {
            if (args != default)
                args = CommonConfig.Concat(args).ToArray();
            else
                args = CommonConfig;
            Config.InitConfig(args, true);

            Init.InitSilo();

            var siloBuilder = new HostBuilder()
                .UseOrleans(builder =>
                {
                    builder.UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = Config.Env;
                        options.ServiceId = "CUGOJ";
                    })
                    .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                    .Configure<ILoggingBuilder>(logging => logging.AddConsole());
                });
            //        .UseOrleans(builder =>
            //        {
            //            builder.Configure<ClusterOptions>(options =>
            //            {
            //                options.ClusterId = Config.Env;
            //                options.ServiceId = "CUGOJ";
            //            })
            //.UseAdoNetClustering(options =>
            //        {
            //            options.ConnectionString = Config.OrleansDBConnectionString;
            //            options.Invariant = "System.Data.SqlClient";
            //        })
            //.ConfigureEndpoints(siloPort: Config.SiloPort, gatewayPort: Config.GatewayPort);
            //            // .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(CUGOJ.Backend.Grains.UserGrain).Assembly).WithReferences())
            //        });
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

            var client =  silo.Services.GetService(typeof(IClusterClient)) as IClusterClient;
            if (client == null)
                throw new Exception("创建客户端失败");
            await action(client);

            await silo.StopAsync();
        }

    }
}
