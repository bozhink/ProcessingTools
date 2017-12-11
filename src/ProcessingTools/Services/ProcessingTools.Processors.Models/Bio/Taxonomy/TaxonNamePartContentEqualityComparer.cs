// <copyright file="TaxonNamePartContentEqualityComparer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Processors.Bio.Taxonomy;

    /// <summary>
    /// <see cref="ITaxonNamePart"/> content equality comparer.
    /// </summary>
    public class TaxonNamePartContentEqualityComparer : IEqualityComparer<ITaxonNamePart>
    {
        /// <inheritdoc/>
        public bool Equals(ITaxonNamePart x, ITaxonNamePart y)
        {
            return x.ContentHash == y.ContentHash;
        }

        /// <inheritdoc/>
        public int GetHashCode(ITaxonNamePart obj)
        {
            return obj.ContentHash;
        }
    }
}
