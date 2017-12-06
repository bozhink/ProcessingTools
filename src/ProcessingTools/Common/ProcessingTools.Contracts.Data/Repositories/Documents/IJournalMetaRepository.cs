// <copyright file="IJournalMetaRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Documents
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.Documents;

    /// <summary>
    /// Journals metadata repository.
    /// </summary>
    public interface IJournalMetaRepository : ICrudRepository<IJournalMeta>
    {
    }
}
