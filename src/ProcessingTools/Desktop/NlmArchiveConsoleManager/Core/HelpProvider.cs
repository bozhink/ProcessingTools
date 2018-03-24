namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Meta;

    public class HelpProvider : IHelpProvider
    {
        private readonly IReporter reporter;
        private readonly IJournalsMetaDataService service;

        public HelpProvider(IReporter reporter, IJournalsMetaDataService service)
        {
            this.reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task GetHelpAsync()
        {
            var journalsMeta = await this.service.GetAllJournalsMetaAsync().ConfigureAwait(false);

            this.reporter.AppendContent("Select a journal with one of these options:");

            journalsMeta.OrderBy(j => j.Permalink)
                .ToList()
                .ForEach(j =>
                {
                    this.reporter.AppendContent($"\t--{j.Permalink}");
                });

            await this.reporter.MakeReportAsync().ConfigureAwait(false);
        }
    }
}
