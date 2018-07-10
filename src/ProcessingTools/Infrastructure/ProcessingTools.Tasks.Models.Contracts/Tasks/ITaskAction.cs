// <copyright file="ITaskAction.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Contracts.Tasks
{
    /// <summary>
    /// Task action.
    /// </summary>
    public interface ITaskAction
    {
        /// <summary>
        /// Gets the task action identifier.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the task identifier.
        /// </summary>
        string TaskId { get; }

        /// <summary>
        /// Gets the order parameter for the task action.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Gets the type of the task action.
        /// </summary>
        int Type { get; }

        /// <summary>
        /// Gets the configuration data of the task action.
        /// </summary>
        string Data { get; }

        /// <summary>
        /// Gets a value indicating whether the task action is active.
        /// </summary>
        bool Active { get; }
    }
}
