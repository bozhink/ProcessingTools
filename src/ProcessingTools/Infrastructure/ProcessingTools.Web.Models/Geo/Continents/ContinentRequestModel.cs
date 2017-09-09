// <copyright file="ContinentRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Continents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Continent request model
    /// </summary>
    public class ContinentRequestModel : IContinent
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the continent object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the continent object.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfContinentName)]
        [MaxLength(ValidationConstants.MaximalLengthOfContinentName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the continent object.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedName)]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets countries.
        /// </summary>
        public ICollection<ICountry> Countries { get; set; } = new HashSet<ICountry>();

        /// <summary>
        /// Gets or sets synonyms.
        /// </summary>
        public ICollection<IContinentSynonym> Synonyms { get; set; } = new HashSet<IContinentSynonym>();
    }
}
