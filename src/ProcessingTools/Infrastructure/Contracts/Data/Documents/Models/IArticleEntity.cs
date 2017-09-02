namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface IArticleEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        string Title { get; }

        DateTime? DateReceived { get; }

        DateTime? DateAccepted { get; }

        DateTime? DatePublished { get; }

        int? Volume { get; }

        int? Issue { get; }

        int? FirstPage { get; }

        int? LastPage { get; }

        string ELocationId { get; }

        Guid JournalId { get; }

        IEnumerable<IDocumentEntity> Documents { get; }

        IEnumerable<IAuthorEntity> Authors { get; }
    }
}
