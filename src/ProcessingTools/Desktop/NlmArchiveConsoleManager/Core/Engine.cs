namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Documents;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Core;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories;
    using ProcessingTools.Services.Contracts.Meta;

    public class Engine : IEngine
    {
        private readonly IHelpProvider helpProvider;
        private readonly IJournalsMetaDataService journalsMetaService;
        private readonly ILogger logger;
        private readonly IProcessorFactory processorFactory;

        public Engine(
            IProcessorFactory processorFactory,
            IJournalsMetaDataService journalsMetaService,
            IHelpProvider helpProvider,
            ILogger<Engine> logger)
        {
            this.processorFactory = processorFactory ?? throw new ArgumentNullException(nameof(processorFactory));
            this.journalsMetaService = journalsMetaService ?? throw new ArgumentNullException(nameof(journalsMetaService));
            this.helpProvider = helpProvider ?? throw new ArgumentNullException(nameof(helpProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private Func<string, bool> FilterDoubleDashedOption => a => a.IndexOf("--") == 0;

        private Func<string, bool> FilterNonDashedOption => a => a.IndexOf("--") < 0;

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
                    this.logger.LogError("'{0}' is not a valid path.", x);
                }

                return null;
            }
        };

        public async Task Run(params string[] args)
        {
            int numberOfDoubleDashedArguments = args.Count(this.FilterDoubleDashedOption);
            if (numberOfDoubleDashedArguments != 1)
            {
                await this.helpProvider.GetHelpAsync().ConfigureAwait(false);
                return;
            }

            var journalId = args.Single(this.FilterDoubleDashedOption).Substring(2);

            IJournalMeta journalMeta = (await this.journalsMetaService.GetAllJournalsMetaAsync().ConfigureAwait(false))
                .FirstOrDefault(j => j.Permalink == journalId);

            if (journalMeta == null)
            {
                this.logger.LogError("Journal not found");
                return;
            }

            this.ProcessDirectories(args, journalMeta);
        }

        private void ProcessDirectories(string[] args, IJournalMeta journalMeta)
        {
            var directories = args.Where(this.FilterNonDashedOption)
                .Select(this.SelectDirectoryName)
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .ToArray();

            foreach (var directoryName in directories)
            {
                this.logger.LogInformation(directoryName);

                var direcoryProcessor = this.processorFactory.CreateDirectoryProcessor(directoryName, journalMeta);

                // Processing of each should be executed strictly sequential
                // due to the changes of the current location.
                direcoryProcessor.Process().Wait();
            }
        }
    }
}
