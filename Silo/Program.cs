using CUGOJ.Tools;
using Orleans.Configuration;
using Orleans.Hosting;
using CUGOJ.Frontend;
using CUGOJ.Silo.Init;

try
{
    Config.InitConfig(args, false);

    await Config.InitRemoteConfig();

    var siloBuilder = WebApplication.CreateBuilder();
    siloBuilder.Host.UseOrleans(builder =>
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
    });
    ServiceInit.InitService(siloBuilder.Services);
    CUGOJ.Frontend.Program.InitBuilder(siloBuilder);

    var silo = siloBuilder.Build();
    ServiceInit.LoadSingletonService(silo.Services);
    
    Init.InitSilo();
    CUGOJ.Frontend.Program.InitWebApplication(silo);

    await silo.RunAsync();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
