﻿// <copyright file="IWriteDocumentHelper.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Write document helper.
    /// </summary>
    public interface IWriteDocumentHelper
    {
        /// <summary>
        /// Write document under specified file name.
        /// </summary>
        /// <param name="outputFileName">Output file name.</param>
        /// <param name="document">Document to be written.</param>
        /// <param name="splitDocument">Whether to split document before write.</param>
        /// <returns>Task.</returns>
        Task<object> WriteAsync(string outputFileName, IDocument document, bool splitDocument);
    }
}
