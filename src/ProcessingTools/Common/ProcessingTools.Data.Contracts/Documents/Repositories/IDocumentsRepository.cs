// <copyright file="IDocumentsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Documents.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Models.Contracts.Documents;

    public interface IDocumentsRepository : ICrudRepository<IDocument>
    {
    }
}
