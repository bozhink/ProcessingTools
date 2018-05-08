// <copyright file="DocumentContentResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    /// <summary>
    /// Document content response model.
    /// </summary>
    public class DocumentContentResponseModel
    {
        /// <summary>
        /// Gets or sets the object ID of the document.
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the object ID of the article.
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the content of the document.
        /// </summary>
        public string Content { get; set; }
    }
}
