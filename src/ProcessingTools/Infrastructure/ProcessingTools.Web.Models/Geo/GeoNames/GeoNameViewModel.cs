// <copyright file="GeoNameViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.GeoNames
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;

    /// <summary>
    /// GeoName view model
    /// </summary>
    public class GeoNameViewModel
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the geo name object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the geo name object.
        /// </summary>
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Strings.NameValidationErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        [StringLength(ValidationConstants.MaximalLengthOfGeoName, ErrorMessageResourceName = nameof(Strings.NameLengthValidationErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.MinimalLengthOfGeoName)]
        public string Name { get; set; }
    }
}
