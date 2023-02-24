// See https://aka.ms/new-console-template for more information
using CommandLine;
using ConsoleAppBaseLine;
using ConsoleAppBaseLine.Configuration;
using DataService;
using DataService.interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;
using System.Reflection;
using System.Runtime.CompilerServices;

ILogger _logger;
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
    /*
      services.AddDbContext<MMContext>(options =>
      {
          options.UseSqlServer(config.GetSection("DataAccess").GetConnectionString(activeConnectionId));
          options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
      });
    */
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
        _logger.Error(e.ToString());
    }


}

void HandleParseError(IEnumerable<Error> errs)
{
    foreach(var error in errs)
    {
        _logger.Error($"{error}");

    }
}