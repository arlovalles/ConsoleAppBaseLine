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
            _log.Debug(AnArg);
        }

        public void DoSomethingElse(string ADifferentArg)
        {
            _log.Debug(ADifferentArg);
        }
    }
}