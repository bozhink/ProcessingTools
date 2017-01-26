namespace ProcessingTools.Services.Data.Services.Meta
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Meta;
    using Models.Meta;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Documents.Data.Common.Contracts.Repositories;
    using ProcessingTools.Extensions.Linq;

    public class JournalsMetaDataServiceWithDatabase : IJournalsMetaDataService
    {
        private readonly IJournalMetaRepository repository;

        public JournalsMetaDataServiceWithDatabase(IJournalMetaRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public async Task<IEnumerable<IJournalMeta>> GetAllJournalsMeta()
        {
            var query = await this.repository.Find(j => true);
            var result = await query.Select(j => new JournalMeta
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
            .ToListAsync();

            return result;
        }
    }
}
