// <copyright file="IDocumentBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Documents
{
    /// <summary>
    /// Document base model.
    /// </summary>
    public interface IDocumentBaseModel : IDescribed
    {
        /// <summary>
        /// Gets the article ID.
        /// </summary>
        string ArticleId { get; }

        /// <summary>
        /// Gets the file ID.
        /// </summary>
        string FileId { get; }

        /// <summary>
        /// Gets the document file.
        /// </summary>
        IDocumentFileModel File { get; }
    }
}
