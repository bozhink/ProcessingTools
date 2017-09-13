namespace ProcessingTools.Services.Data.Services.Meta
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Contracts.Meta;
    using Models.Meta;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Documents;

    public class JournalMetaDataServiceWithFiles : IJournalMetaDataService
    {
        private readonly IDeserializer deserializer;

        public JournalMetaDataServiceWithFiles(IDeserializer deserializer)
        {
            if (deserializer == null)
            {
                throw new ArgumentNullException(nameof(deserializer));
            }

            this.deserializer = deserializer;
        }

        public async Task<IJournalMeta> GetJournalMeta(string journalJsonFileName)
        {
            if (string.IsNullOrWhiteSpace(journalJsonFileName))
            {
                throw new ArgumentNullException(nameof(journalJsonFileName));
            }

            using (var stream = new FileStream(journalJsonFileName, FileMode.Open))
            {
                var journalJsonObject = await this.deserializer.Deserialize<JournalMetaDataContract>(stream).ConfigureAwait(false);

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
