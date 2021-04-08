using log4net;
using log4net.Config;
using log4net.Repository;
using System;

namespace NetCoreLog4Net_NoConfig
{
    class Program
    {
        static void Main(string[] args)
        {

            ILoggerRepository repository = LogManager.CreateRepository("NETCoreFirstRepository");
            BasicConfigurator.Configure(repository);

            ILog logger = LogManager.GetLogger(repository.Name, nameof(Program));

            logger.Info($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:Info");
            logger.Error($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:Error");
            logger.Debug($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:Debug");
            logger.Warn($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:Warn");
            logger.Fatal($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:Fatal");

            Console.ReadKey();
        }
    }
}
