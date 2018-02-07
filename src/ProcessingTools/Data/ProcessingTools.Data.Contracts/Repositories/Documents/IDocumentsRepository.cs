// <copyright file="IDocumentsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Documents
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Documents repository.
    /// </summary>
    public interface IDocumentsRepository : ICrudRepository<IDocument>
    {
    }
}
