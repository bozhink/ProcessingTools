using System.Reflection;
using log4net;
using ProcessingTools.Contracts.Types;
using ProcessingTools.Loggers.Base;
using ProcessingTools.Loggers.Contracts;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "logger.config", Watch = true)]

namespace ProcessingTools.Loggers.Loggers
{
    public class Log4NetLogger : LoggerBase, ILog4NetLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void Log()
        {
        }

        public override void Log(object message) => Logger.Info(message);

        public override void Log(string format, params object[] args) => Logger.InfoFormat(format, args);

        public override void Log(LogType type, object message)
        {
            switch (type)
            {
                case LogType.Info:
                    Logger.Info(message);
                    break;

                case LogType.Warning:
                    Logger.Warn(message);
                    break;

                case LogType.Error:
                    Logger.Error(message);
                    break;

                case LogType.Exception:
                    Logger.Error(message);
                    break;

                default:
                    Logger.Error(message);
                    break;
            }
        }
    }
}
