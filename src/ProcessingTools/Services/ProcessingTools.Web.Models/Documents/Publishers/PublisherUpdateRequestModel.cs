// <copyright file="PublisherUpdateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Publishers
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Journals;
    using ProcessingTools.Services.Models.Contracts.Documents.Publishers;

    /// <summary>
    /// Publisher edit request model.
    /// </summary>
    public class PublisherUpdateRequestModel : IPublisherUpdateModel, ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName, MinimumLength = ValidationConstants.MinimalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfPublisherName, MinimumLength = ValidationConstants.MinimalLengthOfPublisherName)]
        public string Name { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfAddressString, MinimumLength = ValidationConstants.MinimalLengthOfAddressString)]
        public string Address { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
