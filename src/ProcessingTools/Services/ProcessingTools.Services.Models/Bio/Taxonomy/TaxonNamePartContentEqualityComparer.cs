// <copyright file="TaxonNamePartContentEqualityComparer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Services.Models.Bio.Taxonomy;

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
