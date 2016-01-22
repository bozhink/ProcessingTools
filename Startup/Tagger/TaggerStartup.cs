namespace ProcessingTools.MainProgram
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Loggers;

    public class TaggerStartup
    {
        private static ILogger logger = new TextWriterLogger();

        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            Run(args).Wait();

            logger.Log(LogType.Info, "Main timer {0}.", mainTimer.Elapsed);
        }

        private static async Task Run(string[] args)
        {
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
        }
    }
}
