using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace NetCoreLog4Net_UseFileConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("Config/Log4Net.config"));

            ILog logger = LogManager.GetLogger(repository.Name, nameof(Program));

            logger.Info($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Xml Info");
            logger.Error($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Xml Error");
            logger.Debug($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Xml Debug");
            logger.Warn($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Xml Warn");
            logger.Fatal($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Xml Fatal");


            Console.ReadKey();
        }
    }
}
