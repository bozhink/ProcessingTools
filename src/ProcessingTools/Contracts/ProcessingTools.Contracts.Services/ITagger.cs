// <copyright file="ITagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Tagger.
    /// </summary>
    public interface ITagger
    {
        /// <summary>
        /// Asynchronously tag.
        /// </summary>
        /// <returns>Task.</returns>
        Task TagAsync();
    }
}
