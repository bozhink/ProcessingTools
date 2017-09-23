namespace ProcessingTools.Models.Contracts.Documents
{
    public interface IJournalMeta : IPermalinkable
    {
        string JournalId { get; }

        string JournalTitle { get; }

        string AbbreviatedJournalTitle { get; }

        string IssnPPub { get; }

        string IssnEPub { get; }

        string PublisherName { get; }

        /// <summary>
        /// Pattern: {0} = volume, {1} = issue, {2} = id, {3} = first page.
        /// </summary>
        string FileNamePattern { get; }

        string ArchiveNamePattern { get; }
    }
}
