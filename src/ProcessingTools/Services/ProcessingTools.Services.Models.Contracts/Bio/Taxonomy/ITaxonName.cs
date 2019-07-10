﻿// <copyright file="ITaxonName.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Bio.Taxonomy
{
    using System.Linq;
    using ProcessingTools.Common.Enumerations;

    /// <summary>
    /// Taxon name.
    /// </summary>
    public interface ITaxonName
    {
        /// <summary>
        /// Gets id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets position.
        /// </summary>
        long Position { get; }

        /// <summary>
        /// Gets type.
        /// </summary>
        TaxonType Type { get; }

        /// <summary>
        /// Gets taxon name parts. TODO: needs refactoring.
        /// </summary>
        IQueryable<ITaxonNamePart> Parts { get; }
    }
}
