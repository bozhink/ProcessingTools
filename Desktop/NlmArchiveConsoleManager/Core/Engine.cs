namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Core;
    using Contracts.Factories;
    using Contracts.Models;
    using Contracts.Services;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class Engine : IEngine
    {
        private readonly IDirectoryProcessorFactory directoryProcessorFactory;
        private readonly IJournalsMetaDataService journalsMetaService;
        private readonly ILogger logger;

        public Engine(
            IDirectoryProcessorFactory directoryProcessorFactory,
            IJournalsMetaDataService journalsMetaService,
            ILogger logger)
        {
            if (directoryProcessorFactory == null)
            {
                throw new ArgumentNullException(nameof(directoryProcessorFactory));
            }

            if (journalsMetaService == null)
            {
                throw new ArgumentNullException(nameof(journalsMetaService));
            }

            this.directoryProcessorFactory = directoryProcessorFactory;
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
            IJournal journal = await this.journalsMetaService.GetJournalMeta();

            var directories = args.Select(this.SelectDirectoryName)
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .ToArray();

            foreach (var directoryName in directories)
            {
                this.logger?.Log(directoryName);

                var direcoryProcessor = this.directoryProcessorFactory.CreateDirectoryProcessor(directoryName, journal);
                await direcoryProcessor.Process();
            }
        }
    }
}
