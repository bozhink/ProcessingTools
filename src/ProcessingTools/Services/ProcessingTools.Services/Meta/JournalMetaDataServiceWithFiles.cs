// <copyright file="JournalMetaDataServiceWithFiles.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Meta
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Contracts.Services.Meta;
    using ProcessingTools.Contracts.Services.Serialization;
    using ProcessingTools.Services.Models.Data.Meta;

    /// <summary>
    /// Journal meta data service with files.
    /// </summary>
    public class JournalMetaDataServiceWithFiles : IJournalMetaDataService
    {
        private readonly IDeserializer deserializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalMetaDataServiceWithFiles"/> class.
        /// </summary>
        /// <param name="deserializer">Instance of <see cref="IDeserializer"/>.</param>
        public JournalMetaDataServiceWithFiles(IDeserializer deserializer)
        {
            this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }

        /// <inheritdoc/>
        public Task<IJournalMeta> GetJournalMetaAsync(string journalJsonFileName)
        {
            if (string.IsNullOrWhiteSpace(journalJsonFileName))
            {
                throw new ArgumentNullException(nameof(journalJsonFileName));
            }

            return this.GetJournalMetaInternalAsync(journalJsonFileName);
        }

        private async Task<IJournalMeta> GetJournalMetaInternalAsync(string journalJsonFileName)
        {
            await Task.CompletedTask.ConfigureAwait(false);

            using (var stream = new FileStream(journalJsonFileName, FileMode.Open))
            {
                var journalJsonObject = this.deserializer.Deserialize<JournalMetaDataContract>(stream);

                stream.Close();

                return new JournalMeta
                {
                    AbbreviatedJournalTitle = journalJsonObject.AbbreviatedJournalTitle,
                    FileNamePattern = journalJsonObject.FileNamePattern,
                    IssnEPub = journalJsonObject.IssnEPub,
                    IssnPPub = journalJsonObject.IssnPPub,
                    JournalId = journalJsonObject.JournalId,
                    JournalTitle = journalJsonObject.JournalTitle,
                    PublisherName = journalJsonObject.PublisherName,
                };
            }
        }
    }
}
