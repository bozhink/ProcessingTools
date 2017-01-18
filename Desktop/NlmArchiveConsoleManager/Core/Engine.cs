namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Core;
    using Contracts.Factories;
    using Contracts.Settings;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Data.Contracts.Meta;
    using ProcessingTools.Services.Data.Contracts.Models.Meta;

    public class Engine : IEngine
    {
        private readonly IApplicationSettings applicationSettings;
        private readonly IProcessorFactory processorFactory;
        private readonly IJournalsMetaDataService journalsMetaService;
        private readonly ILogger logger;

        public Engine(
            IApplicationSettings applicationSettings,
            IProcessorFactory processorFactory,
            IJournalsMetaDataService journalsMetaService,
            ILogger logger)
        {
            if (applicationSettings == null)
            {
                throw new ArgumentNullException(nameof(applicationSettings));
            }

            if (processorFactory == null)
            {
                throw new ArgumentNullException(nameof(processorFactory));
            }

            if (journalsMetaService == null)
            {
                throw new ArgumentNullException(nameof(journalsMetaService));
            }

            this.applicationSettings = applicationSettings;
            this.processorFactory = processorFactory;
            this.journalsMetaService = journalsMetaService;
            this.logger = logger;
        }

        private Func<string, string> SelectDirectoryName => x =>
        {
            if (Directory.Exists(x))
            {
                return x;
            }
            else
            {
                string path = Path.GetDirectoryName(x);

                if (Directory.Exists(path))
                {
                    return path;
                }
                else
                {
                    logger?.Log(LogType.Error, "'{0}' is not a valid path.", x);
                }

                return null;
            }
        };

        public async Task Run(params string[] args)
        {
            IJournal journal = await this.journalsMetaService.GetJournalMeta(this.applicationSettings.JournalJsonFileName);

            var directories = args.Select(this.SelectDirectoryName)
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .ToArray();

            foreach (var directoryName in directories)
            {
                this.logger?.Log(directoryName);

                var direcoryProcessor = this.processorFactory.CreateDirectoryProcessor(directoryName, journal);

                // Processing of each should be executed strictly sequential
                // due to the changes of the current location.
                direcoryProcessor.Process().Wait();
            }
        }
    }
}
