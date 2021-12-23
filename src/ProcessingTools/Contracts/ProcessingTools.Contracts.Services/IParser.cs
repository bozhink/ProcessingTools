// <copyright file="IParser.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
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
        /// <returns>Task.</returns>
        Task ParseAsync();
    }
}
