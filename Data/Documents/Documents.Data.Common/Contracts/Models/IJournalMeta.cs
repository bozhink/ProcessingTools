namespace ProcessingTools.Documents.Data.Common.Contracts.Models
{
    public interface IJournalMeta
    {
        string JournalId { get; }

        string JournalTitle { get; }

        string AbbreviatedJournalTitle { get; }

        string IssnPPub { get; }

        string IssnEPub { get; }

        string PublisherName { get; }

        string Permalink { get; }

        string FileNamePattern { get; }

        string ArchiveNamePattern { get; }
    }
}
