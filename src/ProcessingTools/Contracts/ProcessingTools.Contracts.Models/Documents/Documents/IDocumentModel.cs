// <copyright file="IDocumentModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Documents
{
    /// <summary>
    /// Document model.
    /// </summary>
    public interface IDocumentModel : IDocumentBaseModel, IStringIdentified, ICreated, IModified
    {
        /// <summary>
        /// Gets a value indicating whether document is final.
        /// </summary>
        bool IsFinal { get; }
    }
}
