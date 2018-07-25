namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Tagger.Contracts;

    public class Engine : IEngine
    {
        private readonly IFileProcessor fileProcessor;
        private readonly ILogger logger;

        public Engine(IFileProcessor fileProcessor, ILogger<Engine> logger)
        {
            this.fileProcessor = fileProcessor ?? throw new ArgumentNullException(nameof(fileProcessor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Run(string[] args)
        {
            if (!int.TryParse(AppSettings.MaximalTimeInMinutesToWaitTheMainThread, out int timeSpanInMunutesValue))
            {
                throw new InvalidCastException("MaximalTimeInMinutesToWaitTheMainThread has invalid value.");
            }

            var ts = TimeSpan.FromMinutes(timeSpanInMunutesValue);

            var succeeded = this.RunAsync(args).Wait(ts);
            if (!succeeded)
            {
                this.logger.LogError("The timeout interval elapsed.");
            }
        }

        private async Task RunAsync(string[] args)
        {
            try
            {
                var settingsBuilder = new ProgramSettingsBuilder(this.logger, args);
                var settings = settingsBuilder.Settings;

                await this.fileProcessor.RunAsync(settings).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, string.Empty);
            }
        }
    }
}
