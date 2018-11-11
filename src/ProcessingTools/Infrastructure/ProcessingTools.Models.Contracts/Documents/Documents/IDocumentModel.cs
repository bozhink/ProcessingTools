// <copyright file="IDocumentModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Documents
{
    /// <summary>
    /// Document model.
    /// </summary>
    public interface IDocumentModel : IDocumentBaseModel, IStringIdentifiable, ICreated, IModified
    {
        /// <summary>
        /// Gets a value indicating whether document is final.
        /// </summary>
        bool IsFinal { get; }
    }
}
