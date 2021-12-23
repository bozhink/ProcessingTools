// <copyright file="MinimalTaxonName.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

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
            if (obj is null)
            {
                return false;
            }

            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}
