// <copyright file="IParser.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
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
