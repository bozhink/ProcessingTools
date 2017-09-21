// <copyright file="ITagger.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
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
        /// <returns>Task</returns>
        Task TagAsync();
    }
}
