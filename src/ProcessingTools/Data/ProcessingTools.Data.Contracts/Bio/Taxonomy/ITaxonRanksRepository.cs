// <copyright file="ITaxonRanksRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks repository.
    /// </summary>
    public interface ITaxonRanksRepository : ICrudRepository<ITaxonRankItem>
    {
    }
}
