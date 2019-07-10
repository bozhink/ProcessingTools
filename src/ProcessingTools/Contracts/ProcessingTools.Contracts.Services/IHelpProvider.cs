// <copyright file="IHelpProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Provider of help content.
    /// </summary>
    public interface IHelpProvider
    {
        /// <summary>
        /// Gets Help content.
        /// </summary>
        /// <returns>Task.</returns>
        Task GetHelpAsync();
    }
}
