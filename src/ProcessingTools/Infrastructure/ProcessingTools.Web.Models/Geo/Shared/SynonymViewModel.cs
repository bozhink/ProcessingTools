// <copyright file="SynonymViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Shared
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;

    /// <summary>
    /// Synonym view model
    /// </summary>
    public class SynonymViewModel
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the synonym object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the synonym object.
        /// </summary>
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(Strings.NameEmptyErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [StringLength(
            maximumLength: ValidationConstants.MaximalLengthOfSynonymName,
            MinimumLength = ValidationConstants.MinimalLengthOfSynonymName,
            ErrorMessageResourceName = nameof(Strings.NameErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        [Display(Name = nameof(Strings.LanguageCode), ResourceType = typeof(Strings))]
        public string LanguageCode { get; set; }
    }
}
