// <copyright file="MinimalTaxonName.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Processors.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Minimal taxon name.
    /// </summary>
    public class MinimalTaxonName : IMinimalTaxonName
    {
        /// <inheritdoc/>
        public IEnumerable<IMinimalTaxonNamePart> Parts { get; set; } = Array.Empty<IMinimalTaxonNamePart>();

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Join(" | ", this.Parts);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}
