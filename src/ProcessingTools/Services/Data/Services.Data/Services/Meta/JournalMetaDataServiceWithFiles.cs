namespace ProcessingTools.Services.Data.Services.Meta
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Contracts.Serialization;
    using ProcessingTools.Contracts.Services.Data.Meta;
    using ProcessingTools.Services.Models.Data.Meta;

    public class JournalMetaDataServiceWithFiles : IJournalMetaDataService
    {
        private readonly IDeserializer deserializer;

        public JournalMetaDataServiceWithFiles(IDeserializer deserializer)
        {
            this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }

        public async Task<IJournalMeta> GetJournalMetaAsync(string journalJsonFileName)
        {
            if (string.IsNullOrWhiteSpace(journalJsonFileName))
            {
                throw new ArgumentNullException(nameof(journalJsonFileName));
            }

            using (var stream = new FileStream(journalJsonFileName, FileMode.Open))
            {
                var journalJsonObject = await this.deserializer.DeserializeAsync<JournalMetaDataContract>(stream).ConfigureAwait(false);

                return new JournalMeta
                {
                    AbbreviatedJournalTitle = journalJsonObject.AbbreviatedJournalTitle,
                    FileNamePattern = journalJsonObject.FileNamePattern,
                    IssnEPub = journalJsonObject.IssnEPub,
                    IssnPPub = journalJsonObject.IssnPPub,
                    JournalId = journalJsonObject.JournalId,
                    JournalTitle = journalJsonObject.JournalTitle,
                    PublisherName = journalJsonObject.PublisherName
                };
            }
        }
    }
}
