﻿// <copyright file="TaxonRankEntity.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank entity.
    /// </summary>
    public class TaxonRankEntity : ITaxonRankItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankEntity"/> class.
        /// </summary>
        public TaxonRankEntity()
        {
            this.Ranks = new HashSet<TaxonRankType>();
        }

        /// <inheritdoc/>
        public bool IsWhiteListed { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public ICollection<TaxonRankType> Ranks { get; }
    }
}
