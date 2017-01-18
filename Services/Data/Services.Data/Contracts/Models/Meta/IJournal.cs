namespace ProcessingTools.Services.Data.Contracts.Models.Meta
{
    public interface IJournal
    {
        string AbbreviatedJournalTitle { get; }

        /// <summary>
        /// Pattern: {0} = volume, {1} = issue, {2} = id, {3} = first page.
        /// </summary>
        string FileNamePattern { get; }

        string IssnEPub { get; }

        string IssnPPub { get; }

        string JournalId { get; }

        string JournalTitle { get; }

        string PublisherName { get; }
    }
}
