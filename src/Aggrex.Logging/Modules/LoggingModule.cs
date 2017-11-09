using Autofac;
using Microsoft.Extensions.Logging;

namespace Aggrex.Logging.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            builder.RegisterInstance<LoggerFactory>(loggerFactory)
                .As<ILoggerFactory>()
                .SingleInstance();
        }
    }
}

