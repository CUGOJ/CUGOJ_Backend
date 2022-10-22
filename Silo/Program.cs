using Orleans.Configuration;
using Orleans.Hosting;
try
{
    CUGOJ.Backend.Tools.Properties.Properties.LoadProperties(args);

    CUGOJ.Backend.Silo.Init.Init.InitSilo();

    var silo = new HostBuilder().UseOrleans(builder =>
    {
        builder.Configure<ClusterOptions>(options =>
        {
            options.ClusterId = Properties.Env;
            options.ServiceId = "CUGOJ";
        })
        .UseAdoNetClustering(options =>
        {
            options.ConnectionString = Properties.OrleansDBConnectionString;
            options.Invariant = "System.Data.SqlClient";
        })
        .ConfigureEndpoints(siloPort: Properties.SiloPort, gatewayPort: Properties.GatewayPort)
        .ConfigureServices(services =>
        {
            services.AddSingleton<>();
        });
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
