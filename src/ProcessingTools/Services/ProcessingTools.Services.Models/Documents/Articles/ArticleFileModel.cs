// <copyright file="ArticleFileModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Articles
{
    using ProcessingTools.Contracts.Services.Models.Documents.Articles;

    /// <summary>
    /// Article file.
    /// </summary>
    public class ArticleFileModel : IArticleFileModel
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
