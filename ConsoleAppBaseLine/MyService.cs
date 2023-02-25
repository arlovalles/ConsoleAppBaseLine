using ConsoleAppBaseLine.Configuration;
using DataService.interfaces;
using NLog;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ConsoleAppBaseLine
{
    public class MyService
    {
        private static readonly NLog.ILogger _log = NLog.LogManager.GetCurrentClassLogger();
        private readonly IMyDataService _myDataService;

        public MyService(IMyDataService dataService)
        {
            _myDataService = dataService;
        }
        public void Run(IConfigurationRoot config, UserCommandLineOptions opts)
        {
            //evaluate Config and Options and Run Accordingly
            try
            {
                _log.Trace($"Executing in MyService. Run Mode = {opts.RunMode} Output File: {opts.OutputFile}");                

                foreach (var inFile in opts.InputFiles)
                {
                    _log.Info($"Input File: {inFile}");
                }

                _myDataService.DoSomething(true);
                _myDataService.DoSomethingElse(opts.OutputFile);


                _log.Warn("Sample Warning : Emphasis Words -> WARNING");
                _log.Error("Sample Error : Emphasis Words -> ERROR");
                _log.Fatal("Sample Fatal : Emphasis Words -> FATAL, FATALITY");


            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
            }

        }
    }
}
