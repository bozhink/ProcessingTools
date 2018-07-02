// <copyright file="ICommandRunner.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Command runner.
    /// </summary>
    public interface ICommandRunner
    {
        /// <summary>
        /// Run a command with specified name.
        /// </summary>
        /// <param name="commandName">Name of the command to be run</param>
        /// <returns>Task</returns>
        Task<object> RunAsync(string commandName);
    }
}
