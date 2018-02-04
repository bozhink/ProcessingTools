// <copyright file="MinimalTaxonNamePart.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Processors.Bio.Taxonomy;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Minimal taxon name part.
    /// </summary>
    public class MinimalTaxonNamePart : IMinimalTaxonNamePart
    {
        private string fullName;
        private string name;
        private SpeciesPartType rank;
        private int contentHash;

        /// <inheritdoc/>
        public virtual string FullName
        {
            get
            {
                return this.fullName;
            }

            set
            {
                this.fullName = value;
                this.UpdateContentHash();
            }
        }

        /// <inheritdoc/>
        public virtual string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.UpdateContentHash();
            }
        }

        /// <inheritdoc/>
        public virtual SpeciesPartType Rank
        {
            get
            {
                return this.rank;
            }

            set
            {
                this.rank = value;
                this.UpdateContentHash();
            }
        }

        /// <inheritdoc/>
        public int ContentHash => this.contentHash;

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "{0} {1}",
                this.Rank.ToString().ToLowerInvariant(),
                string.IsNullOrWhiteSpace(this.FullName) ? this.Name : this.FullName);
        }

        private void UpdateContentHash()
        {
            var contentString = this.FullName + this.Name + this.Rank;
            this.contentHash = contentString.GetHashCode();
        }
    }
}
