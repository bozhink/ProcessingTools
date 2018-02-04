namespace ProcessingTools.Services.Data.Services.Meta
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories.Documents;
    using ProcessingTools.Models.Contracts.Documents;
    using ProcessingTools.Contracts.Services.Data.Meta;
    using ProcessingTools.Services.Models.Data.Meta;

    public class JournalsMetaDataServiceWithDatabase : IJournalsMetaDataService
    {
        private readonly IJournalMetaRepository repository;

        public JournalsMetaDataServiceWithDatabase(IJournalMetaRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IJournalMeta[]> GetAllJournalsMetaAsync()
        {
            var data = await this.repository.FindAsync(j => true).ConfigureAwait(false);
            var result = data.Select(j => new JournalMeta
            {
                AbbreviatedJournalTitle = j.AbbreviatedJournalTitle,
                ArchiveNamePattern = j.ArchiveNamePattern,
                FileNamePattern = j.FileNamePattern,
                IssnEPub = j.IssnEPub,
                IssnPPub = j.IssnPPub,
                JournalId = j.JournalId,
                JournalTitle = j.JournalTitle,
                PublisherName = j.PublisherName
            })
            .ToArray();

            return result;
        }
    }
}
