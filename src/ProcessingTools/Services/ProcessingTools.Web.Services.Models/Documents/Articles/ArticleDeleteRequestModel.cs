// <copyright file="ArticleDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Models.Documents.Articles
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants;

    /// <summary>
    /// Article Delete Request Model
    /// </summary>
    public class ArticleDeleteRequestModel
    {
        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public string Id { get; set; }
    }
}
