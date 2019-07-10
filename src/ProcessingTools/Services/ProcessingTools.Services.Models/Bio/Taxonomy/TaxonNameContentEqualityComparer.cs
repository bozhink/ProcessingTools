// <copyright file="TaxonNameContentEqualityComparer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using ProcessingTools.Services.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// <see cref="ITaxonName"/> content equality comparer.
    /// </summary>
    public class TaxonNameContentEqualityComparer : IEqualityComparer<ITaxonName>
    {
        /// <inheritdoc/>
        public bool Equals(ITaxonName x, ITaxonName y)
        {
            return this.GetHashCode(x) == this.GetHashCode(y);
        }

        /// <inheritdoc/>
        public int GetHashCode(ITaxonName obj)
        {
            return string.Join(".", obj.Parts.Select(p => p.ContentHash)).GetHashCode();
        }
    }
}
