﻿// <copyright file="ITaxonRankItem.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Common.Enumerations;

    /// <summary>
    /// Taxon rank item.
    /// </summary>
    public interface ITaxonRankItem : INamed
    {
        /// <summary>
        /// Gets a value indicating whether entity is included in the white list.
        /// </summary>
        bool IsWhiteListed { get; }

        /// <summary>
        /// Gets ranks.
        /// </summary>
        ICollection<TaxonRankType> Ranks { get; }
    }
}
