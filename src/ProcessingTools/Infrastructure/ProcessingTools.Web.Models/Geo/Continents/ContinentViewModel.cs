// <copyright file="ContinentViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Continents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Web.Models.Geo.Shared;

    /// <summary>
    /// Continent view model
    /// </summary>
    public class ContinentViewModel : SynonymisableViewModel
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the continent object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the continent object.
        /// </summary>
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Strings.NameValidationErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        [StringLength(ValidationConstants.MaximalLengthOfContinentName, ErrorMessageResourceName = nameof(Strings.NameLengthValidationErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.MinimalLengthOfContinentName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the continent object.
        /// </summary>
        [Display(Name = nameof(Strings.AbbreviatedName), ResourceType = typeof(Strings))]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedName, ErrorMessageResourceName = nameof(Strings.AbbreviatedNameValidationErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the number of countries.
        /// </summary>
        [Display(Name = nameof(Strings.NumberOfCountries), ResourceType = typeof(Strings))]
        public int NumberOfCountries { get; set; }

        /// <summary>
        /// Gets or sets countries.
        /// </summary>
        [Display(Name = nameof(Strings.Countries), ResourceType = typeof(Strings))]
        public IEnumerable<CountryViewModel> Countries { get; set; }
    }
}
