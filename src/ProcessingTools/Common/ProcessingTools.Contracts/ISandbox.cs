// <copyright file="ISandbox.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides <see cref="Task"/> sand-boxing methods;
    /// </summary>
    public interface ISandbox
    {
        /// <summary>
        /// Runs an action asynchronously.
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <returns>Task</returns>
        Task RunAsync(Action action);

        /// <summary>
        /// Runs an function asynchronously.
        /// </summary>
        /// <param name="function">Function to be called</param>
        /// <returns>Task</returns>
        Task RunAsync(Func<Task> function);

        /// <summary>
        /// Runs an function asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <param name="function">Function to be called</param>
        /// <returns>Task of result</returns>
        Task<T> RunAsync<T>(Func<Task<T>> function);
    }
}
