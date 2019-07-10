// <copyright file="IFormatter.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Formatter.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Asynchronously format.
        /// </summary>
        /// <returns>Task.</returns>
        Task FormatAsync();
    }
}
