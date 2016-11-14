namespace ProcessingTools.NlmArchiveConsoleManager.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Contracts.Models;
    using Contracts.Services;
    using Contracts.Settings;
    using Models;
    using ProcessingTools.Contracts;

    public class JournalsMetaDataService : IJournalsMetaDataService
    {
        private readonly IApplicationSettings settings;
        private readonly IDeserializer deserializer;

        public JournalsMetaDataService(IApplicationSettings settings, IDeserializer deserializer)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (deserializer == null)
            {
                throw new ArgumentNullException(nameof(deserializer));
            }

            this.settings = settings;
            this.deserializer = deserializer;
        }

        public async Task<IJournal> GetJournalMeta()
        {
            using (var stream = new FileStream(this.settings.JournalJsonFileName, FileMode.Open))
            {
                var journalJsonObject = await this.deserializer.Deserialize<JournalDataContract>(stream);

                return new Journal
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
