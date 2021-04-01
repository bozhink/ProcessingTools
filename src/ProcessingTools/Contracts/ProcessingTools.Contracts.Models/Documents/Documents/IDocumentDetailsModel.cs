// <copyright file="IDocumentDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Documents
{
    /// <summary>
    /// Document details model.
    /// </summary>
    public interface IDocumentDetailsModel : IDocumentModel
    {
        /// <summary>
        /// Gets the number of files.
        /// </summary>
        long NumberOfFiles { get; }
    }
}
