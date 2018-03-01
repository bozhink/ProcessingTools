// <copyright file="JournalsMetaDataServiceWithFiles.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Meta
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Meta;

    /// <summary>
    /// Journals meta data service with files.
    /// </summary>
    public class JournalsMetaDataServiceWithFiles : IJournalsMetaDataService
    {
        private readonly IJournalMetaDataService journalMetaDataService;
        private string journalMetaFilesDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsMetaDataServiceWithFiles"/> class.
        /// </summary>
        /// <param name="journalMetaFilesDirectory">Journal meta files directory.</param>
        /// <param name="journalMetaDataService">Instance of <see cref="IJournalMetaDataService"/>.</param>
        public JournalsMetaDataServiceWithFiles(string journalMetaFilesDirectory, IJournalMetaDataService journalMetaDataService)
        {
            this.JournalMetaFilesDirectory = journalMetaFilesDirectory;
            this.journalMetaDataService = journalMetaDataService ?? throw new ArgumentNullException(nameof(journalMetaDataService));
        }

        private string JournalMetaFilesDirectory
        {
            get
            {
                return this.journalMetaFilesDirectory;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!Directory.Exists(value))
                {
                    throw new DirectoryNotFoundException(value);
                }

                this.journalMetaFilesDirectory = value;
            }
        }

        /// <inheritdoc/>
        public async Task<IJournalMeta[]> GetAllJournalsMetaAsync()
        {
            var journalMetaFiles = Directory.GetFiles(this.JournalMetaFilesDirectory).ToArray();

            var result = new HashSet<IJournalMeta>();
            foreach (var fileName in journalMetaFiles)
            {
                var journalMeta = await this.journalMetaDataService.GetJournalMetaAsync(fileName).ConfigureAwait(false);
                result.Add(journalMeta);
            }

            return result.ToArray();
        }
    }
}
