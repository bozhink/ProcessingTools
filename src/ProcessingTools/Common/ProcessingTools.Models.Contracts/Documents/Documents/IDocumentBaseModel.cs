// <copyright file="IDocumentBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Documents
{
    /// <summary>
    /// Document base model.
    /// </summary>
    public interface IDocumentBaseModel : IDescribed, IContentTyped
    {
        /// <summary>
        /// Gets or sets the document content.
        /// </summary>
        IDocumentContentModel DocumentContent { get; set; }
    }
}

