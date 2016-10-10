namespace ProcessingTools.Tagger
{
    using System;
    using System.Threading.Tasks;

    using Core;

    using ProcessingTools.Contracts;

    public class Startup : IStartup
    {
        private readonly ILogger logger;

        public Startup(ILogger logger)
        {
            this.logger = logger;
        }

        public void Run(string[] args) => this.RunAsync(args).Wait();

        private async Task RunAsync(string[] args)
        {
            try
            {
                var settingsBuilder = new ProgramSettingsBuilder(this.logger, args);
                var settings = settingsBuilder.Settings;

                var singleFileProcessor = DI.Get<ISingleFileProcessor>();

                await singleFileProcessor.Run(settings);
            }
            catch (Exception e)
            {
                this.logger.Log(e, string.Empty);
            }
        }
    }
}
