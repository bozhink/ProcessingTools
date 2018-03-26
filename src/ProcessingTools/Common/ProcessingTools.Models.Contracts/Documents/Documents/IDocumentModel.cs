// <copyright file="IDocumentModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Documents
{
    /// <summary>
    /// Document model.
    /// </summary>
    public interface IDocumentModel : IDocumentBaseModel, IStringIdentifiable, ICreated, IModified
    {
        /// <summary>
        /// Gets or sets a value indicating whether document is final.
        /// </summary>
        bool IsFinal { get; set; }
    }
}

