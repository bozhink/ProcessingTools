namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Models
{
    public interface IJournal
    {
        string JournalId { get; set; }

        string JournalTitle { get; set; }

        string AbbreviatedJournalTitle { get; set; }

        string IssnPPub { get; set; }

        string IssnEPub { get; set; }

        string PublisherName { get; set; }

        /// <summary>
        /// Structure must be: {0} = volume, {1} = issue, {2} = id, {3} = first page.
        /// </summary>
        string FileNamePattern { get; set; }
    }
}