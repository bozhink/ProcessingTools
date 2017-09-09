// <copyright file="CountryViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Continents
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;

    /// <summary>
    /// Country view model
    /// </summary>
    public class CountryViewModel
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the country object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the country object.
        /// </summary>
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Strings.NameValidationErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        [StringLength(ValidationConstants.MaximalLengthOfCountryName, ErrorMessageResourceName = nameof(Strings.NameLengthValidationErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.MinimalLengthOfCountryName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        [Display(Name = nameof(Strings.LanguageCode), ResourceType = typeof(Strings))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Strings.LanguageCodeValidationErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        [StringLength(ValidationConstants.MaximalLengthOfLanguageCode, ErrorMessageResourceName = nameof(Strings.LanguageCodeLengthValidationErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.MinimalLengthOfLanguageCode)]
        public string LanguageCode { get; set; }
    }
}
