namespace ProcessingTools.Documents.Data.Common.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IJournalMeta : IPermalinkable
    {
        string JournalId { get; }

        string JournalTitle { get; }

        string AbbreviatedJournalTitle { get; }

        string IssnPPub { get; }

        string IssnEPub { get; }

        string PublisherName { get; }

        string FileNamePattern { get; }

        string ArchiveNamePattern { get; }
    }
}
