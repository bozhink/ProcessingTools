// <copyright file="IArticleFileModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Documents.Articles
{
    /// <summary>
    /// Article file model.
    /// </summary>
    public interface IArticleFileModel
    {
        /// <summary>
        /// Gets the raw Content-Type header of the uploaded file.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Gets the raw Content-Disposition header of the uploaded file.
        /// </summary>
        string ContentDisposition { get; }

        /// <summary>
        /// Gets the file length in bytes.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Gets the name from the Content-Disposition header.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the file name from the Content-Disposition header.
        /// </summary>
        string FileName { get; }
    }
}
