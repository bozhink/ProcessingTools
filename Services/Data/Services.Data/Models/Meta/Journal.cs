namespace ProcessingTools.Services.Data.Models.Meta
{
    using System.Text.RegularExpressions;
    using Contracts.Models.Meta;

    public class Journal : IJournal
    {
        public string AbbreviatedJournalTitle { get; set; }

        /// <summary>
        /// Structure must be: {0} = volume, {1} = issue, {2} = id, {3} = first page.
        /// </summary>
        public string FileNamePattern { get; set; }

        public string IssnEPub { get; set; }

        public string IssnPPub { get; set; }

        public string JournalId { get; set; }

        public string JournalTitle { get; set; }

        public string PublisherName { get; set; }

        public string Permalink => Regex.Replace(this.AbbreviatedJournalTitle, @"\W+", "_").ToLower();
    }
}
