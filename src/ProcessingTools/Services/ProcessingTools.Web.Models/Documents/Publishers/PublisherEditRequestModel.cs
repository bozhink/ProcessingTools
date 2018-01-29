// <copyright file="PublisherEditRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Publishers
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Journals;

    /// <summary>
    /// Publisher edit request model.
    /// </summary>
    public class PublisherEditRequestModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the publisher's abbreviated name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName, MinimumLength = ValidationConstants.MinimalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the publisher's name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfPublisherName, MinimumLength = ValidationConstants.MinimalLengthOfPublisherName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the publisher's address string.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfAddressString, MinimumLength = ValidationConstants.MinimalLengthOfAddressString)]
        public string Address { get; set; }
    }
}
