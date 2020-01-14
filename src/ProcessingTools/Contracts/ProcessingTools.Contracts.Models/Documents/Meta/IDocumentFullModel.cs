// <copyright file="IDocumentFullModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Meta
{
    using ProcessingTools.Contracts.Models.Documents.Documents;

    /// <summary>
    /// Document full model.
    /// </summary>
    public interface IDocumentFullModel : IArticleFullModel
    {
        /// <summary>
        /// Gets the document.
        /// </summary>
        IDocumentModel Document { get; }
    }
}
