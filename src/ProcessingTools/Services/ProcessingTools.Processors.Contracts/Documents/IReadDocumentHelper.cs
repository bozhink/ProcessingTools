// <copyright file="IReadDocumentHelper.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Read document helper.
    /// </summary>
    public interface IReadDocumentHelper
    {
        /// <summary>
        /// Read document[s].
        /// </summary>
        /// <param name="mergeInputFiles">Merge input files.</param>
        /// <param name="fileNames">Names of files to be read.</param>
        /// <returns>Read document.</returns>
        Task<IDocument> ReadAsync(bool mergeInputFiles, params string[] fileNames);
    }
}
