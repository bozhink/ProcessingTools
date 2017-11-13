// <copyright file="ITaxonNamePart.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Contracts.Bio.Taxonomy.Parsers
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Taxon name part.
    /// </summary>
    public interface ITaxonNamePart : IMinimalTaxonNamePart
    {
        /// <summary>
        /// Gets id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets a value indicating whether taxon name part is abbreviated.
        /// </summary>
        bool IsAbbreviated { get; }

        /// <summary>
        /// Gets or sets a value indicating whether taxon name part is modified.
        /// </summary>
        bool IsModified { get; set; }

        /// <summary>
        /// Gets a value indicating whether shortened taxon name part is resolved.
        /// </summary>
        bool IsResolved { get; }

        /// <summary>
        /// Gets match expression.
        /// </summary>
        Expression<Func<ITaxonNamePart, bool>> MatchExpression { get; }

        /// <summary>
        /// Gets pattern.
        /// </summary>
        string Pattern { get; }
    }
}
