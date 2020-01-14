// <copyright file="ITagger.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Threading.Tasks;

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
