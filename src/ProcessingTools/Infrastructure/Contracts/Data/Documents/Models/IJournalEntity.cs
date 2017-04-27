namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using System;
    using ProcessingTools.Contracts.Models;

    public interface IJournalEntity : IAbbreviatedNameableGuidIdentifiable, IModelWithUserInformation
    {
        string JournalId { get; }

        string PrintIssn { get; }

        string ElectronicIssn { get; }

        Guid PublisherId { get; }
    }
}
