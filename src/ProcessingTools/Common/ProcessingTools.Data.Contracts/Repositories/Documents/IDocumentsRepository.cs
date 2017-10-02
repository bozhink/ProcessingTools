// <copyright file="IDocumentsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Documents
{
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Documents repository.
    /// </summary>
    public interface IDocumentsRepository : ICrudRepository<IDocument>
    {
    }
}
