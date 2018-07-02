// <copyright file="IQueryReplacer.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Query replacer.
    /// </summary>
    public interface IQueryReplacer
    {
        /// <summary>
        /// Does multiple replaces using a valid XML query.
        /// </summary>
        /// <param name="content">Content to be processed.</param>
        /// <param name="query">Query string or query file name containing [multiple ]replace instructions.</param>
        /// <returns>Task of the processed content.</returns>
        Task<string> ReplaceAsync(string content, string query);
    }
}
