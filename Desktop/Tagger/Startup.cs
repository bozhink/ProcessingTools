namespace ProcessingTools.Tagger
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Ninject;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Loggers;

    public class Startup
    {
        private readonly ILogger logger;

        public Startup()
        {
            this.logger = new ConsoleLogger();
        }

        public static Startup Create()
        {
            return new Startup();
        }

        public void Run(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            using (IKernel kernel = NinjectConfig.CreateKernel())
            {
                this.RunAsync(kernel, args).Wait();
            }

            this.logger.Log(LogType.Info, "Main timer {0}.", mainTimer.Elapsed);
        }

        public async Task RunAsync(IKernel kernel, string[] args)
        {
            try
            {
                var settingsBuilder = new ProgramSettingsBuilder(this.logger, args);
                var settings = settingsBuilder.Settings;

                var singleFileProcessor = new SingleFileProcessor(settings, this.logger);

                await singleFileProcessor.Run(kernel);
            }
            catch (Exception e)
            {
                this.logger.Log(e, string.Empty);
            }
        }
    }
}
