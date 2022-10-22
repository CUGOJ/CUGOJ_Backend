using CUGOJ.Backend.Silo.Init;
using CUGOJ.Backend.Tools.Params;
using Orleans.Configuration;
using Orleans.Hosting;
try
{
    Config.LoadProperties(args);

    Init.InitSilo();

    var silo = new HostBuilder().UseOrleans(builder =>
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
    }).Build();
    await silo.StartAsync();
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

    app.Run();

    await silo.StopAsync();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
