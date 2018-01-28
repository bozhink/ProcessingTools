// <copyright file="PublisherDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Publishers
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants;

    /// <summary>
    /// Publisher Delete Request Model
    /// </summary>
    public class PublisherDeleteRequestModel
    {
        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public string Id { get; set; }
    }
}
