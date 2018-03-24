// <copyright file="IParser.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Parser.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Asynchronously parse.
        /// </summary>
        /// <returns>Task</returns>
        Task ParseAsync();
    }
}
