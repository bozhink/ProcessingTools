namespace ProcessingTools.MainProgram
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Loggers;

    public class Startup
    {
        private readonly ILogger logger;

        public Startup()
        {
            this.logger = new TextWriterLogger();
        }

        public static Startup Create()
        {
            return new Startup();
        }

        public void Run(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            this.RunAsync(args).Wait();

            this.logger.Log(LogType.Info, "Main timer {0}.", mainTimer.Elapsed);
        }

        public async Task RunAsync(string[] args)
        {
            try
            {
                var settingsBuilder = new ProgramSettingsBuilder(this.logger, args);
                var settings = settingsBuilder.Settings;

                var singleFileProcessor = new SingleFileProcessor(settings, this.logger);

                await singleFileProcessor.Run();
            }
            catch (Exception e)
            {
                this.logger.Log(e, string.Empty);
            }
        }
    }
}
