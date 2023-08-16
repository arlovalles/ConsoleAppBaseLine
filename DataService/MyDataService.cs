using DataService.interfaces;
using NLog;
using System.Reflection;
using EFData;

namespace DataService
{
    public class MyDataService : IMyDataService
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        private MyDataContext _DataContext;

        public MyDataService(MyDataContext context) { 
            
            _DataContext = context;
        
        }
        
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