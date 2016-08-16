namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

    public interface IJournalEntity : IAbbreviatedNameableGuidIdentifiable, IModelWithUserInformation
    {
        string JournalId { get; }

        string PrintIssn { get; }

        string ElectronicIssn { get; }

        Guid PublisherId { get; }

        IPublisherEntity Publisher { get; }

        ICollection<IArticleEntity> Articles { get; }
    }
}
