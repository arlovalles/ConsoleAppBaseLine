using DataService.interfaces;
using NLog;
using System.Reflection;

namespace DataService
{
    public class MyDataService : IMyDataService
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();

        public void DoSomething(bool AnArg)
        {
            _log.Debug($"Logging {AnArg} in DoSomething()");
        }

        public void DoSomethingElse(string ADifferentArg)
        {
            _log.Debug($"Logging {ADifferentArg} in DoSomethingElse()");
        }
    }
}