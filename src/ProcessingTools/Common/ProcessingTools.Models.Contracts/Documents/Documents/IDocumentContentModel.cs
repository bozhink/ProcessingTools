// <copyright file="IDocumentContentModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Documents
{
    /// <summary>
    /// Document content model.
    /// </summary>
    public interface IDocumentContentModel
    {
        /// <summary>
        /// Gets the document object ID.
        /// </summary>
        string DocumentId { get; }

        /// <summary>
        /// Gets the content of the document.
        /// </summary>
        string Content { get; }
    }
}
