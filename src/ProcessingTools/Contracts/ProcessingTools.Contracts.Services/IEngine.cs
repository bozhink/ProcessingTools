// <copyright file="IEngine.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Engine of the main functional unit.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Run the execution of the engine.
        /// </summary>
        /// <param name="args">Arguments for the run.</param>
        /// <returns>Task.</returns>
        Task RunAsync(string[] args);
    }
}
