namespace ProcessingTools.Services.Data.Models.Meta
{
    using System.Text.RegularExpressions;
    using ProcessingTools.Contracts.Models.Documents;

    internal class JournalMeta : IJournalMeta
    {
        public string AbbreviatedJournalTitle { get; set; }

        public string ArchiveNamePattern { get; set; }

        /// <summary>
        /// Gets or sets the file name pattern. Pattern is: {0} = volume, {1} = issue, {2} = id, {3} = first page.
        /// </summary>
        public string FileNamePattern { get; set; }

        public string IssnEPub { get; set; }

        public string IssnPPub { get; set; }

        public string JournalId { get; set; }

        public string JournalTitle { get; set; }

        public string Permalink => Regex.Replace(this.AbbreviatedJournalTitle, @"\W+", "_").ToLowerInvariant();

        public string PublisherName { get; set; }
    }
}
