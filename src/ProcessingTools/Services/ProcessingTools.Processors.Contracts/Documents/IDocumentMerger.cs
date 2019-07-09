// <copyright file="IDocumentMerger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document merger.
    /// </summary>
    public interface IDocumentMerger
    {
        /// <summary>
        /// Merge multiple documents specified by file name into one <see cref="IDocument"/> instance.
        /// </summary>
        /// <param name="fileNames">Names of the files to be merged.</param>
        /// <returns>Merged document.</returns>
        Task<IDocument> MergeAsync(params string[] fileNames);
    }
}
