// <copyright file="JournalsMetaDataServiceWithDatabase.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Meta
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Models.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Meta;
    using ProcessingTools.Services.Models.Data.Meta;

    /// <summary>
    /// Journals meta data service with database.
    /// </summary>
    public class JournalsMetaDataServiceWithDatabase : IJournalsMetaDataService
    {
        private readonly IJournalMetaDataAccessObject dataAccessObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsMetaDataServiceWithDatabase"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Instance of <see cref="IJournalMetaDataAccessObject"/>.</param>
        public JournalsMetaDataServiceWithDatabase(IJournalMetaDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
        }

        /// <inheritdoc/>
        public async Task<IJournalMeta[]> GetAllJournalsMetaAsync()
        {
            var data = await this.dataAccessObject.GetAllAsync().ConfigureAwait(false);
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
