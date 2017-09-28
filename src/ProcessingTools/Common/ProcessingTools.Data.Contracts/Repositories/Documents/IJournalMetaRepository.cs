// <copyright file="IJournalMetaRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Documents
{
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Journals metadata repository.
    /// </summary>
    public interface IJournalMetaRepository : ICrudRepository<IJournalMeta>
    {
    }
}
