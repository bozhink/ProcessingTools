namespace ProcessingTools.MainProgram
{
    using System;
    using System.Diagnostics;

    public partial class MainProcessingTool
    {
        public const int NumberOfExpandingIterations = 1;

        private static ILogger logger = new ConsoleLogger();

        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            try
            {
                var settingsBuilder = new ProgramSettingsBuilder(logger, args);
                var settings = settingsBuilder.Settings;

                var singleFileProcessor = new SingleFileProcessor(settings, logger);

                singleFileProcessor.Run().Wait();
            }
            catch (Exception e)
            {
                logger.LogException(e, string.Empty);
            }

            logger.Log("Main timer: " + mainTimer.Elapsed);
        }
    }
}