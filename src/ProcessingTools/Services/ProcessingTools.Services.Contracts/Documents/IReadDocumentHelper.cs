// <copyright file="IReadDocumentHelper.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services.Documents
{
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
