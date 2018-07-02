// <copyright file="IHelpProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Provider of help content.
    /// </summary>
    public interface IHelpProvider
    {
        /// <summary>
        /// Gets Help content.
        /// </summary>
        /// <returns>Task</returns>
        Task GetHelpAsync();
    }
}
