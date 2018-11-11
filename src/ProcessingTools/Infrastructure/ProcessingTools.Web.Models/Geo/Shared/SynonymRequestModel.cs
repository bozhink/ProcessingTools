// <copyright file="SynonymRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Shared
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Synonym request model
    /// </summary>
    public class SynonymRequestModel : IGeoSynonym
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the synonym object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the synonym object.
        /// </summary>
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
        public int? LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the update status of the synonym.
        /// </summary>
        public UpdateStatus Status { get; set; } = UpdateStatus.NotModified;

        /// <summary>
        /// Gets or sets the ID of the parent object.
        /// </summary>
        public int ParentId { get; set; } = -1;
    }
}
