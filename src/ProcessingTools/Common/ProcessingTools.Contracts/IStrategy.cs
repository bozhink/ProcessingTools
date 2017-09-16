// <copyright file="IStrategy.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    /// <summary>
    /// Represent a strategy of execution.
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// Gets the execution priority of the strategy.
        /// </summary>
        int ExecutionPriority { get; }
    }
}
