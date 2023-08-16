// See https://aka.ms/new-console-template for more information
using CommandLine;
using ConsoleAppBaseLine;
using ConsoleAppBaseLine.Configuration;
using DataService;
using DataService.interfaces;
using EFData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

NLog.ILogger _logger;
string currentDir = Directory.GetCurrentDirectory();

var config = new ConfigurationBuilder()
                .SetBasePath(currentDir)
                .AddJsonFile("appsettings.json").Build();

ConfigureLogger(config);

IServiceCollection services = ConfigureServices(config);

ServiceProvider serviceProvider = services.BuildServiceProvider();

CommandLine.Parser.Default.ParseArguments<UserCommandLineOptions>(args)
    .WithParsed(RunOptions)
    .WithNotParsed(HandleParseError);



void ConfigureLogger(IConfigurationRoot config)
{

    LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

    _logger = LogManager.GetCurrentClassLogger();

}

IServiceCollection ConfigureServices(IConfigurationRoot config)
{
    IServiceCollection services = new ServiceCollection();

    _logger.Debug("Configuring Services");

    var activeConnectionId = config.GetSection("DataAccess").GetSection("ActiveConnectionId").Value;

    _logger.Warn($"Active Connection : {activeConnectionId}");

    //Services
    services.AddTransient<MyService>();
    services.AddTransient<IMyDataService, MyDataService>();

    //Add EF context
    
      services.AddDbContext<MyDataContext>(options =>
      {
          options.UseLoggerFactory((ILoggerFactory)LogManager.LogFactory);
          options.UseSqlServer(config.GetSection("DataAccess").GetConnectionString(activeConnectionId));
          options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
      });
    
    return services;
}

void RunOptions(UserCommandLineOptions opts)
{
    try
    {
        serviceProvider.GetService<MyService>().Run(config, opts);
    }
    catch(Exception e)
    {
        _logger.Fatal(e.ToString());
    }


}

void HandleParseError(IEnumerable<Error> errs)
{
    foreach(var error in errs)
    {
        _logger.Fatal($"{error}");

    }
}