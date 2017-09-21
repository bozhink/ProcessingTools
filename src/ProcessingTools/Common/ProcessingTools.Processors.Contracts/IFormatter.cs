// <copyright file="IFormatter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Formatter.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Asynchronously format.
        /// </summary>
        /// <returns>Task</returns>
        Task FormatAsync();
    }
}
