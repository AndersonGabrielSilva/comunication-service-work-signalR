using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using WorkerDomain.Settings;
using WorkerService1;


IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "Woker Hub Service";
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<WorkerServiceSettings>(sp =>
        {
            var _configuration = sp.GetService<IConfiguration>();
            return _configuration.GetSection(nameof(WorkerServiceSettings)).Get<WorkerServiceSettings>(); ;
        });

        services.AddHostedService<Worker>();

        LoggerProviderOptions.RegisterProviderOptions<
            EventLogSettings, EventLogLoggerProvider>(services);

    })
    .Build();

await host.RunAsync();
