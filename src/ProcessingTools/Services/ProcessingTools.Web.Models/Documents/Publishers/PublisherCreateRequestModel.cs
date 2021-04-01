// <copyright file="PublisherCreateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Journals;
    using ProcessingTools.Contracts.Services.Models.Documents.Publishers;

    /// <summary>
    /// Publisher create request model.
    /// </summary>
    public class PublisherCreateRequestModel : IPublisherInsertModel, ProcessingTools.Contracts.Models.IWebModel
    {
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
        public Uri ReturnUrl { get; set; }
    }
}
