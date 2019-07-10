﻿// <copyright file="ICommandRunner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Command runner.
    /// </summary>
    public interface ICommandRunner
    {
        /// <summary>
        /// Run a command with specified name.
        /// </summary>
        /// <param name="commandName">Name of the command to be run.</param>
        /// <returns>Task.</returns>
        Task<object> RunAsync(string commandName);
    }
}
