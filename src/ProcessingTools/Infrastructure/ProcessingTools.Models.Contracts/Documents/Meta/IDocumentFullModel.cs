// <copyright file="IDocumentFullModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Meta
{
    using ProcessingTools.Models.Contracts.Documents.Documents;

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
