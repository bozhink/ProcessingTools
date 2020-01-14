﻿// <copyright file="ArticleFileRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using ProcessingTools.Contracts.Services.Models.Documents.Articles;

    /// <summary>
    /// Article file request model.
    /// </summary>
    public class ArticleFileRequestModel : IArticleFileModel
    {
        /// <inheritdoc/>
        public string ContentType { get; set; }

        /// <inheritdoc/>
        public string ContentDisposition { get; set; }

        /// <inheritdoc/>
        public long Length { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string FileName { get; set; }
    }
}
