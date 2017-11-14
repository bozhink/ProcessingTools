// <copyright file="Species.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.Taxonomy
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Species mode.
    /// </summary>
    public class Species
    {
        private readonly Regex genusNameMatchInXml = new Regex("(?<=type=\"genus\"[^>]*>)[A-Z][a-z\\.]+(?=</t)");
        private readonly Regex subgenusNameMatchInXml = new Regex("(?<=type=\"subgenus\"[^>]*>)[A-Z][a-z\\.]+(?=</t)");
        private readonly Regex speciesNameMatchInXml = new Regex("(?<=type=\"species\"[^>]*>)[a-z\\-\\.]+(?=</t)");
        private readonly Regex subspeciesNameMatchInXml = new Regex("(?<=type=\"subspecies\"[^>]*>)[a-z\\-]+(?=</t)");

        private readonly string genus;
        private readonly string subgenus;
        private readonly string species;
        private readonly string subspecies;

        /// <summary>
        /// Initializes a new instance of the <see cref="Species"/> class.
        /// </summary>
        /// <param name="parsedContent">Value of the parsed content.</param>
        public Species(string parsedContent)
        {
            Match m = this.genusNameMatchInXml.Match(parsedContent);
            this.genus = m.Success ? m.Value : string.Empty;

            m = this.subgenusNameMatchInXml.Match(parsedContent);
            this.subgenus = m.Success ? m.Value : string.Empty;

            m = this.speciesNameMatchInXml.Match(parsedContent);
            this.species = m.Success ? m.Value : string.Empty;

            m = this.subspeciesNameMatchInXml.Match(parsedContent);
            this.subspecies = m.Success ? m.Value : string.Empty;
        }

        /// <summary>
        /// Gets the name of the genus.
        /// </summary>
        public string GenusName => this.genus;

        /// <summary>
        /// Gets the name of the subgenus.
        /// </summary>
        public string SubgenusName => this.subgenus;

        /// <summary>
        /// Gets the name of the species.
        /// </summary>
        public string SpeciesName => this.species;

        /// <summary>
        /// Gets the name of the subspecies.
        /// </summary>
        public string SubspeciesName => this.subspecies;

        /// <summary>
        /// Gets the genus pattern.
        /// </summary>
        public string GenusPattern
        {
            get
            {
                return (this.genus.IndexOf('.') > -1) ? "\\b" + this.genus.Substring(0, this.genus.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + this.genus + "\\b";
            }
        }

        /// <summary>
        /// Gets the subgenus pattern.
        /// </summary>
        public string SubgenusPattern
        {
            get
            {
                return (this.subgenus.IndexOf('.') > -1) ? "\\b" + this.subgenus.Substring(0, this.subgenus.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + this.subgenus + "\\b";
            }
        }

        /// <summary>
        /// Gets the species pattern.
        /// </summary>
        public string SpeciesPattern
        {
            get
            {
                return (this.species.IndexOf('.') > -1) ? "\\b" + this.species.Substring(0, this.species.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + this.species + "\\b";
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string name = (this.genus.Length == 0) ? string.Empty : this.genus;
            name += (this.subgenus.Length == 0) ? string.Empty : " (" + this.subgenus + ")";
            name += (this.species.Length == 0) ? string.Empty : " " + this.species;
            name += (this.subspecies.Length == 0) ? string.Empty : " " + this.subspecies;
            return name;
        }
    }
}
