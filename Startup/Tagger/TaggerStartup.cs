namespace ProcessingTools.MainProgram
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading.Tasks;

    using Ninject;
    using ProcessingTools.Contracts.Log;
    using ProcessingTools.Loggers;

    public class TaggerStartup
    {
        private static ILogger logger = new TextWriterLogger();

        public static void Main(string[] args)
        {
            Run(args).Wait();
        }

        private static async Task Run(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            try
            {
                var settingsBuilder = new ProgramSettingsBuilder(logger, args);
                var settings = settingsBuilder.Settings;

                var singleFileProcessor = new SingleFileProcessor(settings, logger);

                await singleFileProcessor.Run();
            }
            catch (Exception e)
            {
                logger.Log(e, string.Empty);
            }

            logger.Log(LogType.Info, "Main timer {0}.", mainTimer.Elapsed);
        }
    }
}