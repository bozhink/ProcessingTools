using System;
using System.Reflection;
using log4net;
using ProcessingTools.Enumerations;
using ProcessingTools.Loggers.Base;
using ProcessingTools.Loggers.Contracts;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "logger.config", Watch = true)]

namespace ProcessingTools.Loggers.Loggers
{
    public class Log4NetLogger : LoggerBase, ILog4NetLogger
    {
        private readonly ILog logger;

        public Log4NetLogger()
        {
            this.logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public Log4NetLogger(Type type)
        {
            this.logger = LogManager.GetLogger(type);
        }

        public override void Log()
        {
        }

        public override void Log(object message) => logger.Info(message);

        public override void Log(string format, params object[] args) => logger.InfoFormat(format, args);

        public override void Log(LogType type, object message)
        {
            switch (type)
            {
                case LogType.Debug:
                    this.logger.Debug(message);
                    break;

                case LogType.Info:
                    this.logger.Info(message);
                    break;

                case LogType.Warning:
                    this.logger.Warn(message);
                    break;

                case LogType.Error:
                    this.logger.Error(message);
                    break;

                case LogType.Fatal:
                    this.logger.Fatal(message);
                    break;

                case LogType.Exception:
                    this.logger.ErrorFormat("Exception: {0}", message);
                    break;

                default:
                    this.logger.ErrorFormat("Default Error: {0}", message);
                    break;
            }
        }
    }
}
