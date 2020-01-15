// <copyright file="Country.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Country entity.
    /// </summary>
    public class Country : BaseModel, ISynonymisable<CountrySynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets or sets the ID of the country entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfCountryName)]
        [MaxLength(ValidationConstants.MaximalLengthOfCountryName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the country.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedName)]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the calling code of the country.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfCallingCode)]
        public string CallingCode { get; set; }

        /// <summary>
        /// Gets or sets the language code of the country.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfLanguageCode)]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the ISO639x code of the country.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfIso639xCode)]
        public string Iso639xCode { get; set; }

        /// <summary>
        /// Gets the collection of continent entities.
        /// </summary>
        public virtual ICollection<Continent> Continents { get; private set; } = new HashSet<Continent>();

        /// <summary>
        /// Gets the collection of the state entities.
        /// </summary>
        public virtual ICollection<State> States { get; private set; } = new HashSet<State>();

        /// <summary>
        /// Gets the collection of the province entities.
        /// </summary>
        public virtual ICollection<Province> Provinces { get; private set; } = new HashSet<Province>();

        /// <summary>
        /// Gets the collection of the region entities.
        /// </summary>
        public virtual ICollection<Region> Regions { get; private set; } = new HashSet<Region>();

        /// <summary>
        /// Gets the collection of the district entities.
        /// </summary>
        public virtual ICollection<District> Districts { get; private set; } = new HashSet<District>();

        /// <summary>
        /// Gets the collection of the municipality entities.
        /// </summary>
        public virtual ICollection<Municipality> Municipalities { get; private set; } = new HashSet<Municipality>();

        /// <summary>
        /// Gets the collection of the County entities.
        /// </summary>
        public virtual ICollection<County> Counties { get; private set; } = new HashSet<County>();

        /// <summary>
        /// Gets the collection of the city entities.
        /// </summary>
        public virtual ICollection<City> Cities { get; private set; } = new HashSet<City>();

        /// <summary>
        /// Gets the collection of synonym entities.
        /// </summary>
        public virtual ICollection<CountrySynonym> Synonyms { get; private set; } = new HashSet<CountrySynonym>();
    }
}
