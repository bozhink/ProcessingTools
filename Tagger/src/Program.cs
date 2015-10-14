namespace ProcessingTools.MainProgram
{
    using System;
    using System.Diagnostics;

    public partial class MainProcessingTool
    {
        public const int NumberOfExpandingIterations = 1;

        private static ProgramSettings settings = new ProgramSettings();
        private static ILogger consoleLogger = new ConsoleLogger();

        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            try
            {
                var settingsBuilder = new ProgramSettingsBuilder(args);
                settings = settingsBuilder.Settings;

                var singleFileProcessor = new SingleFileProcessor(settings, consoleLogger);

                singleFileProcessor.Run().Wait();
            }
            catch (Exception e)
            {
                consoleLogger.LogException(e, string.Empty);
            }

            consoleLogger.Log("Main timer: " + mainTimer.Elapsed);
        }
    }
}