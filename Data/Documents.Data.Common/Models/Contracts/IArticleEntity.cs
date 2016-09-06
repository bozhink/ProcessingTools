﻿namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts;

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

        ICollection<IDocumentEntity> Documents { get; }

        ICollection<IAuthorEntity> Authors { get; }
    }
}
